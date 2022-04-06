using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
 
 
    private float ZoomAmount = 0; //With Positive and negative values
 public float MaxToClamp = 10;
 public float ROTSpeed = 10;
 
    void Update()
    {
        var camera = Camera.main;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            
            if (camera.fieldOfView > 1)
            {
                camera.fieldOfView--;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (camera.fieldOfView < 100)
            {
                camera.fieldOfView++;
            }
        }





        
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(0))
         return;
 
        Vector3 pos = camera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
 
        camera.transform.Translate(-move, Space.World); 
    }


 }

