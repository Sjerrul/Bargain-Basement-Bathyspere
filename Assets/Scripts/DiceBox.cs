using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBox : MonoBehaviour
{
    public Sprite[] Faces;

    public GameObject UiDicePrefab;

    public GameObject[] UiDice;

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

        for (int i = 0; i < NumberOfDice; i++)
        {
            UiDice[i] = Instantiate(UiDicePrefab, Vector3.zero, Quaternion.identity, this.transform);

            Die die =  UiDice[i].GetComponent<Die>();
            if (die == null)
            {
                throw new MissingComponentException("Missing Die script on DiePrefab");
            }

            int roll = Random.Range(1, 7);
            die.Value = roll;

            UiDice[i].GetComponent<Image>().sprite = Faces[roll - 1];
        }
    }
}
