using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public int Value;

    public void OnSelect()
    {
        Debug.Log("Selected DIe: " + this.Value);
    }
}
