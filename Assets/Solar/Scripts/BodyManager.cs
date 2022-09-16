using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class BodyManager : MonoBehaviour
{
    private PlanetInfoManager infoManager;
    public CelestialBody selectedBody = null;

    public TextAsset simData;

    [HideInInspector]
    public float G = 6.673f * Mathf.Pow(10.0f, -11.0f);

    public float timeMultiplier = 1f;

    CelestialBody[] bodies;
    private float _GravConstant = 6.673f * Mathf.Pow(10.0f, -11.0f); // m3 kg-1 s-2
    private float _SimTime = 0.0f;

    private float _DistanceConversion = 6.68458712f * Mathf.Pow(10.0f, -12.0f); // m -> AU (Astronomical Units)
    private float _MassConversion = 1.67403241f * Mathf.Pow(10.0f, -25.0f); // kg -> E (Earth masses)
    private float _TimeConversion = 1.15740741f * Mathf.Pow(10.0f, -5.0f); // s -> D (Days)
    private bool slowtime = false;

    public void slowTime()
    {
        slowtime = true;
    }

    public void normalTime()
    {
        slowtime = false;
    }

    public void setSelectedBody(CelestialBody b)
    {
        if (selectedBody == b)
            selectedBody = null;
        else
            selectedBody = b;
    }

    private void Start()
    {
        bodies = GameObject.FindObjectsOfType<CelestialBody>();
        infoManager = GameObject.Find("PlanetInfoManager").GetComponent<PlanetInfoManager>();
        
        _GravConstant *= Mathf.Pow(_DistanceConversion, 3.0f);
        _GravConstant /= _MassConversion;
        _GravConstant /= Mathf.Pow(_TimeConversion, 2.0f);

        Time.timeScale = timeMultiplier;
        //BuildSimulation();
    }


    private void FixedUpdate()
    {
        infoManager.hidePanel();

        if (!slowtime)
            Time.timeScale = timeMultiplier;
        else Time.timeScale = 1f;

        normalTime();
        
        updateVelocities();
        updatePositions();

        if (selectedBody != null)
        {
            infoManager.setNameField(selectedBody.GetComponent<BodyData>().bodyName);
            infoManager.setDistanceField(selectedBody.transform.position.magnitude);
            infoManager.setMassField(selectedBody.mass);
            infoManager.setSpeedField(selectedBody.velocity.magnitude);
            infoManager.showPanel();
            slowTime();
        }

        _SimTime += Time.fixedDeltaTime;
    }

    //private void BuildSimulation()
    //{
    //    XmlDocument data = new XmlDocument();
    //    data.LoadXml(simData.text);
    //
    //    for(int i = 0; i < data.GetElementsByTagName("body").Count; i++)
    //    {
    //        
    //    }
    //}

    private void updateVelocities()
    {
        for(int i = 0; i < bodies.Length; i++)
        {
            Vector3 forceSum = Vector3.zero;
            for(int j = 0; j < bodies.Length; j++) 
            {
                if (bodies[i] == bodies[j]) continue;
                Vector3 separationVector = bodies[j].transform.position - bodies[i].transform.position;
                float forceValue = (_GravConstant * bodies[i].mass * bodies[j].mass) / separationVector.sqrMagnitude;
                forceSum += separationVector.normalized * forceValue;
            }
            bodies[i].acceleration = forceSum / bodies[i].mass;
            bodies[i].velocity += bodies[i].acceleration * Time.fixedDeltaTime;
        }
    }

    private void updatePositions()
    {
        foreach(CelestialBody b in bodies)
        {
            b.transform.position += b.velocity * Time.fixedDeltaTime;
        }
    }
}
