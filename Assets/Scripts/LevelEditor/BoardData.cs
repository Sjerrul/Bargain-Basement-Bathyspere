using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataFile/BoardData")]
public class BoardData : ScriptableObject
{
    public SquareData[] squares;
}
