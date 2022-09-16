using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class solaryCamera : MonoBehaviour
{
    public CelestialBody target;
    public float targetDistance = 100;
    private float distance = 100;
    public float zoomSpeed = 4;
    public float lerpSpeed = 4;
    private bool dragging = false;
    private Vector3 prevPos = new Vector3();

    public float close = 0.01f;
    public float far = 10f;

    public void setTargetBody(CelestialBody b)
    {
        target = b;
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButtonUp(1))
        {
            dragging = false;
        }

        if (dragging)
        {
            Vector3 dir = prevPos - Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Camera.main.transform.position = target.transform.position;
            Camera.main.transform.Rotate(new Vector3(1, 0, 0), dir.y * 180);
            Camera.main.transform.Rotate(new Vector3(0, 1, 0), -dir.x * 180, Space.World);
            Camera.main.transform.Translate(new Vector3(0, 0, -distance));
            prevPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        else
        {
            Camera.main.transform.position = target.transform.position;
            Camera.main.transform.Translate(new Vector3(0, 0, -distance));
        }


        if (Input.GetMouseButtonDown(1))
        {
            dragging = true;
            prevPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void HandleZoom()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            targetDistance -= (targetDistance * zoomSpeed) * Time.unscaledDeltaTime;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetDistance += (targetDistance * zoomSpeed) * Time.unscaledDeltaTime;
        }

        targetDistance = Mathf.Clamp(targetDistance, close, far);
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();

        distance = Mathf.Lerp(distance, targetDistance, Time.unscaledDeltaTime * lerpSpeed);
    }
}
