using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyData : MonoBehaviour
{
    public string bodyName = "name";
    public float textSize = 14f;
    public bool hideIfFar = false;
    public float hideDist = 1f;


    public int maxDataSize = 5000;

    public ScrollingBuffer positionLog;


    public bool visualizeTrajectory = true;
    public float logInterval = 1f;
    public BodyData visualiseRelativeTo;
    //public Color trajectoryColor = Color.white;
    public Gradient trajectoryColor;

    private float timer = 0;

    private void Start() {
        positionLog = new ScrollingBuffer(maxDataSize);
    }

    //private void Update() {
        //text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        //lineRenderer.positionCount = 0;
        //
        //
        //lineRenderer.positionCount = positionLog.Data.Count;
        //for (int i = 0; i < positionLog.Data.Count; i++) {
        //    Vector3 position = Camera.main.WorldToScreenPoint(positionLog.Data[(positionLog.Offset + i) % positionLog.MaxSize]);
        //    if (position.z < 0) { // DIRTTY DIRTY TODO: LICK BALLS
        //        lineRenderer.positionCount = 0;
        //        break;
        //    }
        //    position.x = ((position.x / Camera.main.pixelWidth) * 8.888f) - 4.444f;
        //    position.x *= 2;
        //    position.y = ((position.y / Camera.main.pixelHeight) * 5) - 2.5f;
        //    position.y *= 2;
        //    position.z = 0;
        //    lineRenderer.SetPosition(i, position);
        //}
    //}

    private void FixedUpdate() {
        if (visualizeTrajectory && timer > logInterval)
        {
            timer = 0;
            if (visualiseRelativeTo)
            {
                positionLog.AddPoint(transform.position - visualiseRelativeTo.transform.position);
            }else
                positionLog.AddPoint(transform.position);
        }

        timer += Time.fixedDeltaTime;
    }
}
