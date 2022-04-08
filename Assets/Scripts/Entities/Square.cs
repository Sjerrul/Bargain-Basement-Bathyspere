using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class Square : MonoBehaviour
{
    public string Reference  = $"{Guid.NewGuid()}";
    public bool IsSelected { get; private set;}
    public Square PreviousSquare;
    public Square NextSquare;

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

        var line = this.transform.Find("Line");
        var box = this.transform.Find("Box");
        Debug.Log(line);
        if (NextSquare != null)
        {
            Strech(line.gameObject, this.transform.position, this.NextSquare.transform.position, box.rotation, mirrorZ: true);
        }

    }

    void OnValidate()
    {
          var line = this.transform.Find("Line");
        var box = this.transform.Find("Box");
        Debug.Log(line);
        if (NextSquare != null)
        {
            Strech(line.gameObject, this.transform.position, this.NextSquare.transform.position, box.rotation, mirrorZ: true);
        }
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
            this.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
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

     public void Strech(GameObject sprite,Vector3 initialPosition, Vector3 finalPosition, Quaternion rotation, bool mirrorZ) {
         Debug.Log("Stretline");
         Vector3 centerPos = (initialPosition + finalPosition) / 2f;
         sprite.transform.position = centerPos;
         Vector3 direction = finalPosition - initialPosition;
         direction = Vector3.Normalize(direction);
         sprite.transform.up = direction;
         

         if (mirrorZ) sprite.transform.right *= -1f;
         Vector3 scale = new Vector3(1,1,1);
         scale.y = Vector3.Distance(initialPosition, finalPosition);
         sprite.transform.localScale = scale;
     }
}
