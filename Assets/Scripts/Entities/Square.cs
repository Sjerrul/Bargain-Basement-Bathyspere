using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public bool IsMarked { get; private set;}
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
        if (IsMarked)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
    }
    
    public void SetMarked(bool isMarked)
    {
        this.IsMarked = isMarked;
    }
}
