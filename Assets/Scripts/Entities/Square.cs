using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Square : MonoBehaviour
{
    public string Reference  = $"{Guid.NewGuid()}";
    public bool IsSelected { get; private set;}
    public bool IsMarked { get; private set;}
    public Square PreviousSquare;
    public Square NextSquare;

    public int StressModifier;
    public int OxygenModifier;

    public int DamageModifier;
   
    void OnValidate()
    {
        UpdateLine();
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
        UpdateLine();
    }
    public void SetMarked(bool isMarked)
    {
        this.IsMarked = isMarked;
    }

    public void SetSelected(bool isSelected)
    {
        this.IsSelected = isSelected;
    }

     private void Strech(GameObject sprite, Vector3 initialPosition, Vector3 finalPosition, Quaternion rotation, bool mirrorZ) {
         Vector3 centerPos = (initialPosition + finalPosition) / 2f;
         sprite.transform.position = centerPos;

         sprite.transform.localScale = new Vector3(1, 1, 1);
         sprite.transform.rotation = Quaternion.identity;
         sprite.transform.RotateAround(centerPos, new Vector3(1,0,0), 90);

         Vector3 direction = initialPosition - finalPosition;
   
        float angle = Vector3.Angle(direction, transform.forward);
        sprite.transform.RotateAround(centerPos, new Vector3(0,1,0), angle);


        //  if (mirrorZ) sprite.transform.right *= -1f;
          Vector3 scale = new Vector3(1,1,1);
          scale.y = Vector3.Distance(initialPosition, finalPosition);
          scale.y *= 0.6f;
          sprite.transform.localScale = scale;
     }

    private void UpdateLine()
    {
        var line = this.transform.Find("Line");
        var box = this.transform.Find("Box");
        if (NextSquare != null)
        {
            Strech(line.gameObject, this.transform.position, this.NextSquare.transform.position, box.rotation, mirrorZ: true);
        }
    }

     private void UpdateModifiers()
    {
        var stressLabel = this.transform.Find("Modifiers/Stress/Text").gameObject.GetComponent<TextMesh>();
        if (StressModifier != 0)
        {
            this.transform.Find("Modifiers/Stress").gameObject.SetActive(true);
            stressLabel.text = $"-{this.StressModifier}";
        }
        else
        {
            this.transform.Find("Modifiers/Stress").gameObject.SetActive(false);
        }
        
        var oxygenLabel = this.transform.Find("Modifiers/Oxygen/Text").gameObject.GetComponent<TextMesh>();
        if (OxygenModifier != 0)
        {
            this.transform.Find("Modifiers/Oxygen").gameObject.SetActive(true);
            oxygenLabel.text = $"-{this.OxygenModifier}";
        }
        else
        {
            this.transform.Find("Modifiers/Oxygen").gameObject.SetActive(false);
            oxygenLabel.text = string.Empty;
        }

        var damageLabel = this.transform.Find("Modifiers/Damage/Text").gameObject.GetComponent<TextMesh>();
        if (DamageModifier != 0)
        {
            this.transform.Find("Modifiers/Damage").gameObject.SetActive(true);
            damageLabel.text = $"-{this.DamageModifier}";
        }
        else
        {
            this.transform.Find("Modifiers/Damage").gameObject.SetActive(false);
            damageLabel.text = string.Empty;
        }
    }
}
