using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Token : MonoBehaviour
{
    public Square CurrentSquare {get; private set; }

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (CurrentSquare == null)
        {
            return;
        }
            
        if (Vector3.Distance(this.transform.position, this.CurrentSquare.transform.position) > 0.01f)
        {
            Debug.Log("Token :: Moving");
            transform.position = Vector3.SmoothDamp(this.transform.position, this.CurrentSquare.transform.position, ref this.velocity, this.smoothTime);
        }
    }   

    public void SetCurrentSquare(Square currentSquare)
    {
        this.CurrentSquare = currentSquare;
    }
}
