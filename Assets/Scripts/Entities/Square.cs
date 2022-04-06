using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Square : MonoBehaviour
{
    public bool IsSelected { get; private set;}
    // public Square NextSquare;
    // public Square PreviousSquare;

    public int StressModifier;
    public int OxygenModifier;
    
    void Start()
    {
        StringBuilder text = new StringBuilder();
        if (StressModifier != 0)
        {
            text.AppendLine($"S-{this.StressModifier}");
        }

        if (OxygenModifier != 0)
        {
            text.AppendLine($"O-{this.OxygenModifier}");
        }

        this.GetComponentInChildren<TextMesh>().text = text.ToString();
    }

    void Update()
    {
        var meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        if (IsSelected)
        {
            // if (meshRenderer.material != SelectedMaterial)
            // {
            //     meshRenderer.material = SelectedMaterial;
            // }
            this.transform.position = new Vector3(this.transform.position.x, 0.02f, this.transform.position.z);
        }
        else
        {
            // if (meshRenderer.material != StandardMaterial)
            // {
            //     meshRenderer.material = StandardMaterial;
            // }
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
    }
    
    public void SetMarked(bool isSelected)
    {
        this.IsSelected = isSelected;
    }
}
