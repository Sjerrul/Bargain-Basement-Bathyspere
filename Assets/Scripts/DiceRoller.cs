using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBox : MonoBehaviour
{
    public Sprite[] Faces;

    public GameObject UiDicePrefab;

    public GameObject[] UiDice;

    public int[] Values;

    public int NumberOfDice;

    public void RollDice()
    {
        Debug.Log("Rolling");

        // Destroy Any exisiting UiDice
        for (int i = 0; i < UiDice.Length; i++)
        {
            GameObject.Destroy(UiDice[i]);
        }

        UiDice = new GameObject[this.NumberOfDice];
        Values = new int[this.NumberOfDice];

        for (int i = 0; i < NumberOfDice; i++)
        {
            UiDice[i] = Instantiate(UiDicePrefab, Vector3.zero, Quaternion.identity, this.transform);

            int roll = Random.Range(1, 7);
            Values[i] = roll;

            UiDice[i].GetComponent<Image>().sprite = Faces[roll - 1];
        }
       


    }
}
