using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SquareData
{
    public string Reference;
    
    public float[] Rotation;
    public float[] Position;
    public int OxygenModifier;
    public int StressModifier;
    public int DamageModifier;

    public string NextSquare;
    public string PreviousSquare;
    public string Name; 
    public int SquareType;

    public bool IsDepthZone;

    public SquareData(Square square)
    {
        this.Reference = square.Reference;
        this.OxygenModifier = square.OxygenModifier;
        this.StressModifier = square.StressModifier;
        this.DamageModifier = square.DamageModifier;

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

        this.NextSquare = square.NextSquare?.Reference;
        this.PreviousSquare = square.PreviousSquare?.Reference;

        this.SquareType = (int)square.SquareType;
        this.IsDepthZone = square.IsDepthZone;
    }
}
