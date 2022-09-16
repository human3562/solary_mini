using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(debugTrajectoryPlotter))]
//public class debugTrajectoryInspector : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//
//        debugTrajectoryPlotter dp = (debugTrajectoryPlotter)target;
//        if(GUILayout.Button("Generate trajectories"))
//        {
//            dp.GenerateTrajectories();
//        }
//    }
//}


public class VirtualBody
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float mass;

    public VirtualBody(Vector3 pos, Vector3 vel, float m)
    {
        position = pos; velocity = vel; mass = m;
    }
}


[ExecuteInEditMode]
public class debugTrajectoryPlotter : MonoBehaviour
{
    Vector3[,] trajectories;
    public int iterations = 0;
    public float dt = 0.01f;
    private int bodyCount = 0;

    private float G;


    private float _DistanceConversion = 6.68458712f * Mathf.Pow(10.0f, -12.0f); // m -> AU (Astronomical Units)
    private float _MassConversion = 1.67403241f * Mathf.Pow(10.0f, -25.0f); // kg -> E (Earth masses)
    private float _TimeConversion = 1.15740741f * Mathf.Pow(10.0f, -5.0f); // s -> D (Days)

    public CelestialBody relativeTo;

    [ExecuteInEditMode]
    private void Update()
    {
        if(!Application.isPlaying)
            GenerateTrajectories();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            GenerateTrajectories();
    }

    public void GenerateTrajectories()
    {
        G = GetComponent<BodyManager>().G;

        G *= Mathf.Pow(_DistanceConversion, 3.0f);
        G /= _MassConversion;
        G /= Mathf.Pow(_TimeConversion, 2.0f);

        CelestialBody[] originalBodies = GameObject.FindObjectsOfType<CelestialBody>();
        VirtualBody[] bodies = new VirtualBody[originalBodies.Length];

        Vector3 relativeToInitialPos = Vector3.zero;
        VirtualBody vRelativeTo = null;
        //if (relativeTo != null) relativeToInitialPos = relativeTo.transform.position;
        
        for(int i = 0; i < originalBodies.Length; i++)
        {
            bodies[i] = new VirtualBody(originalBodies[i].transform.position, originalBodies[i].initialVelocity, originalBodies[i].mass);
            if (originalBodies[i] == relativeTo)
            {
                relativeToInitialPos = bodies[i].position;
                vRelativeTo = bodies[i];
            }
        }
        
        
        
        
        bodyCount = bodies.Length;
        trajectories = new Vector3[bodies.Length, iterations];
        


        
        for (int iteration = 0; iteration < iterations; iteration++)
        {
        
            for (int i = 0; i < bodies.Length; i++)
            {
                Vector3 forceSum = Vector3.zero;
                for (int j = 0; j < bodies.Length; j++)
                {
                    if (bodies[i] == bodies[j]) continue;
                    Vector3 separationVector = bodies[j].position - bodies[i].position;
                    float forceValue = (G * bodies[i].mass * bodies[j].mass) / separationVector.sqrMagnitude;
                    forceSum += separationVector.normalized * forceValue;
                }
                bodies[i].acceleration = forceSum / bodies[i].mass;
                bodies[i].velocity += bodies[i].acceleration * dt;
            }
        
            for (int i = 0; i < bodies.Length; i++)
            {
                bodies[i].position += bodies[i].velocity * dt;
                if (vRelativeTo != null)
                {
                    Vector3 offset = vRelativeTo.position - relativeToInitialPos;
                    trajectories[i, iteration] = bodies[i].position - offset;
                }
                else
                    trajectories[i, iteration] = bodies[i].position;
                if(relativeTo && relativeTo == originalBodies[i]) trajectories[i, iteration] = relativeToInitialPos;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if(trajectories != null)
        for (int i = 0; i < trajectories.GetLength(0); i++)
        {
            for (int j = 1; j < trajectories.GetLength(1); j++)
            {
                Gizmos.DrawLine(trajectories[i, j - 1], trajectories[i, j]);
            }
        }
    }
}
