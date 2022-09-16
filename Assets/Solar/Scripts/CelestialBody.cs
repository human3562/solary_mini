using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass;

    public Vector3 initialVelocity;
    
    public Vector3 velocity;
    public Vector3 acceleration;

    private void Start()
    {
        velocity = initialVelocity;
    }

    //public Vector3 forceToBody(CelestialBody other, float G)
    //{
    //    float F = G * other.mass * mass / Vector3.SqrMagnitude(other.transform.position - transform.position);
    //    Vector3 force = (other.transform.position - transform.position).normalized * F;
    //    return force;
    //}
}
