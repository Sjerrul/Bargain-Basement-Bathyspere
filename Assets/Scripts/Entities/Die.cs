using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    public int Value;

    public bool IsSelected {get; private set; }

    void Update()
    {
        if (IsSelected)
        {
            this.GetComponent<Image>().color = Color.red;
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    public void SetSelected(bool selected)
    {
        this.IsSelected = selected;
    }
}
