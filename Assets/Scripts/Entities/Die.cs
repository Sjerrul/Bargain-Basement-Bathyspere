using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public event Action<Die> Click;

    public int Value;

    void OnSelect()
    {
        Debug.Log("Die " + Value);
        Click(this);
    }
}
