using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "DataFile/BoardData")]
public class BoardData : ScriptableObject
{
    public List<SquareData> Squares;
}
