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
    public Square PreviousSquare;
    public Square NextSquare;

    public int StressModifier;
    public int OxygenModifier;

    void Start()
    {

    }

    private void UpdateText()
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
    }

    void OnValidate()
    {
        UpdateLine();
        UpdateText();
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
            box.color = Color.white;
        }

        UpdateText();
        UpdateLine();
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

    public void SetMarked(bool isSelected)
    {
        this.IsSelected = isSelected;
    }

     public void Strech(GameObject sprite, Vector3 initialPosition, Vector3 finalPosition, Quaternion rotation, bool mirrorZ) {
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
}
