using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform Target;

    public Vector3 Offset;
    public float SmoothSpeed;

    public float ScrollSpeed;

    private Vector3 lastMousePosition;

    private bool dragging = true;

    void Start()
    {
        this.Offset = Camera.main.transform.position;
    }

    void Update()
    {
        var camera = Camera.main;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Offset -= new Vector3(0, ScrollSpeed, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Offset += new Vector3(0, ScrollSpeed, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.lastMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            dragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }


    void FixedUpdate()
    {
        var camera = Camera.main;

        if (!dragging)
        {
            var desiredPosition = Target.position + Offset;    

            var smoothedPosition = Vector3.Lerp(camera.transform.position, desiredPosition,  SmoothSpeed);
            camera.transform.position = smoothedPosition;
        }
        else
        {

        }
        
    }
 }

