using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform Target;

    public Vector3 offset;
    public float smoothSPeed;

    void Update()
    {
        var camera = Camera.main;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            offset -= new Vector3(0, 0.1f, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            offset += new Vector3(0, 0.1f, 0);
        }
    }

    void FixedUpdate()
    {
        var camera = Camera.main;

        var desiredPosition = Target.position + offset;
        var smoothedPosition = Vector3.Lerp(camera.transform.position, desiredPosition,  smoothSPeed);
        camera.transform.position = smoothedPosition;
    }
 }

