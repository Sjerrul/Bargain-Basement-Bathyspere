using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBox : MonoBehaviour
{
    public Sprite[] Faces;

    public GameObject UiDicePrefab;

    List<GameObject> dice;

    void Start()
    {
        dice = new List<GameObject>();
    }

    public void ClearDice()
    {
        for (int i = 0; i < dice.Count; i++)
        {
            GameObject.Destroy(dice[i]);
        }
    }

    public void AddDie(int value, Action<Die> onClickAction)
    {
        GameObject die = new GameObject($"Die: {value}", typeof(Image), typeof(Die), typeof(UnityEngine.UI.Button));
        die.transform.SetParent(this.transform);
        
        die.GetComponent<Image>().sprite = Faces[value - 1];
        die.GetComponent<Die>().Value = value;
        die.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => onClickAction(die.GetComponent<Die>()));

        dice.Add(die);
    }
}
