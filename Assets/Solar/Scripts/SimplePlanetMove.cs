using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlanetMove : MonoBehaviour
{
    public float distance;
    public float speed;

    private void Update()
    {
        Vector3 newpos = new Vector3(Mathf.Cos(Time.realtimeSinceStartup * speed) * distance, 0, Mathf.Sin(Time.realtimeSinceStartup * speed) * distance);
        transform.position = newpos;
    }
}
