using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineManager : MonoBehaviour{
    public GameObject linePrefab;
    public GameObject planetNameTextPrefab;

    TextMeshProUGUI[] names;
    BodyData[] bodies;
    LineRenderer[] lines;

    private void Start() {
        bodies = FindObjectsOfType<BodyData>();
        lines = new LineRenderer[bodies.Length];
        names = new TextMeshProUGUI[bodies.Length];
        for(int i = 0; i < bodies.Length; i++) {
            lines[i] = Instantiate(linePrefab, transform).GetComponent<LineRenderer>();
            names[i] = Instantiate(planetNameTextPrefab, GameObject.Find("PlanetNames").transform).GetComponent<TextMeshProUGUI>();
            names[i].text = bodies[i].bodyName;
            lines[i].colorGradient = bodies[i].trajectoryColor;
        }
    }

    //private void LateUpdate()
    //{
    //    
    //}

    private void LateUpdate() {
        for(int i = 0; i < bodies.Length; i++) {
            names[i].enabled = true;
            Vector3 namePos = Camera.main.WorldToScreenPoint(bodies[i].transform.position);
            if (namePos.z < 0 || (bodies[i].hideIfFar && Vector3.Distance(Camera.main.transform.position, bodies[i].transform.position) > bodies[i].hideDist))
            {
                names[i].enabled = false;
            }
            else
            {
                namePos.x += names[i].rectTransform.rect.width / 2;
                names[i].transform.position = namePos;
                names[i].fontSize = bodies[i].textSize;
            }
            //names[i].transform.position = Camera.main.WorldToScreenPoint(bodies[i].transform.position);
            

            lines[i].positionCount = 0;
            lines[i].enabled = true;
            if (!bodies[i].visualizeTrajectory) continue;
            lines[i].positionCount = bodies[i].positionLog.Data.Count+1;
            for (int j = 0; j < bodies[i].positionLog.Data.Count; j++) {
                Vector3 worldPos = bodies[i].positionLog.Data[(bodies[i].positionLog.Offset + j) % bodies[i].positionLog.MaxSize];
                if (bodies[i].visualiseRelativeTo)
                {
                    worldPos += bodies[i].visualiseRelativeTo.transform.position;
                }
                Vector3 position = Camera.main.WorldToScreenPoint(worldPos);
                if (position.z < 0) { // DIRTTY DIRTY TODO: LICK BALLS
                    lines[i].enabled = false;
                    break;
                }
                position.x = ((position.x / Camera.main.pixelWidth) * 8.888f) - 4.444f;
                position.x *= 2;
                position.y = ((position.y / Camera.main.pixelHeight) * 5) - 2.5f;
                position.y *= 2;
                position.z = 0;
                lines[i].SetPosition(j, position);
            }
            Vector3 endPos = Camera.main.WorldToScreenPoint(bodies[i].transform.position);
            endPos.x = ((endPos.x / Camera.main.pixelWidth) * 8.888f) - 4.444f;
            endPos.x *= 2;
            endPos.y = ((endPos.y / Camera.main.pixelHeight) * 5) - 2.5f;
            endPos.y *= 2;
            endPos.z = 0;
            lines[i].SetPosition(bodies[i].positionLog.Data.Count, endPos);
        }
    }
}
