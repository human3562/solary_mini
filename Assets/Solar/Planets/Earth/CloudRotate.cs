using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotate : MonoBehaviour
{
    public GameObject clouds;
    public Vector3 rotation;

    private void Update() {
        clouds.transform.Rotate(rotation * Time.deltaTime);
    }
}
