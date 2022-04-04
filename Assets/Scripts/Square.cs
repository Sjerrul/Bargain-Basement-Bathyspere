using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public event Action<Square> MouseClickEvent; 

    public Square NextSquare;
    public Square PreviousSquare;

    public int PowerModifier;
    public int OxygenModifier;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        if (MouseClickEvent != null)
        {
            MouseClickEvent(this);
        }
    }
}
