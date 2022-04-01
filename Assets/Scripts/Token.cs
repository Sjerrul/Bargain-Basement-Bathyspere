using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    public Square CurrentSquare;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    
    void OnMouseUp()
    {
        Debug.Log("Click Token");
        this.CurrentSquare = this.CurrentSquare.NextSquare;
        this.transform.position = this.CurrentSquare.transform.position;
    }
}
