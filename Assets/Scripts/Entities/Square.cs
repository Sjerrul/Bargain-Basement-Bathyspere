using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        StringBuilder text = new StringBuilder();
        if (PowerModifier != 0)
        {
            text.AppendLine($"P-{this.PowerModifier}");
        }

        if (OxygenModifier != 0)
        {
            text.AppendLine($"O-{this.OxygenModifier}");
        }

        this.GetComponentInChildren<TextMesh>().text = text.ToString();
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
