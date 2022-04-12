using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum SquareType
{
    Regular = 0,
    Start  = 1,
    Goal  = 2
}

[Serializable]
public class Square : MonoBehaviour
{
    public string Reference  = $"{Guid.NewGuid()}";
    public bool IsSelected { get; private set;}
    public bool IsMarked { get; private set;}
    public Square PreviousSquare;
    public Square NextSquare;

    public SquareType SquareType;

    public int StressModifier;
    public int OxygenModifier;

    public int DamageModifier;

    public bool IsDepthZone;
   
    void OnValidate()
    {
        UpdateModifiers();

    }

    void Update()
    {
        var box = this.transform.Find("Box").gameObject.GetComponent<SpriteRenderer>();
        

        if (IsSelected)
        {
            box.color = Color.blue;
        }
        else
        {
            if (IsMarked)
            {
                box.color = Color.grey;
            }
            else
            {
                box.color = Color.white;
            }
        }

        UpdateModifiers();
    }
    public void SetMarked(bool isMarked)
    {
        this.IsMarked = isMarked;
    }

    public void SetSelected(bool isSelected)
    {
        this.IsSelected = isSelected;
    }

    private void UpdateModifiers()
    {
        UpdateModifier("Modifiers/Stress", this.StressModifier);
        UpdateModifier("Modifiers/Oxygen", this.OxygenModifier);
        UpdateModifier("Modifiers/Damage", this.DamageModifier);

        var depthBar = this.transform.Find("DeptZoneBar").gameObject.GetComponent<SpriteRenderer>();
        depthBar.enabled = this.IsDepthZone;
    }

    private void UpdateModifier(string path, int modifier)
    {
        if (modifier != 0 && !this.IsMarked)
        {
            this.transform.Find(path).gameObject.SetActive(true);
            
            var label = this.transform.Find(path + "/Text").gameObject.GetComponent<TextMesh>();
            label.text = $"-{modifier}";
        }
        else
        {
            this.transform.Find(path).gameObject.SetActive(false);
        }
    }
}
