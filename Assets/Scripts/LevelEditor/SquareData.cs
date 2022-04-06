using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SquareData
{
    public float[] Rotation;
    public float[] Position;
    public int OxygenModifier;
    public int StressModifier;

    public string Name; 

    public SquareData(Square square)
    {
        this.OxygenModifier = square.OxygenModifier;
        this.StressModifier = square.StressModifier;

        this.Position = new float[3]
        {
            square.transform.position.x,
            square.transform.position.y,
            square.transform.position.z,
        };

        this.Rotation = new float[4]
        {
            square.transform.rotation.x,
            square.transform.rotation.y,
            square.transform.rotation.z,
            square.transform.rotation.w
        };

        this.Name = square.name;
    }
}
