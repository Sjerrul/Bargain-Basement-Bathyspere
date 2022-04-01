using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Sprite[] Faces;

    public GameObject UiDicePrefab;

    public GameObject[] UiDice;

    public int NumberOfDice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void RollDice()
    {
        Debug.Log("Rolling");

        for (int i = 0; i < UiDice.Length; i++)
        {
            GameObject.Destroy(UiDice[i]);
        }

        UiDice = new GameObject[this.NumberOfDice];
        for (int i = 0; i < NumberOfDice; i++)
        {
            UiDice[i] = Instantiate(UiDicePrefab, Vector3.zero, Quaternion.identity, this.transform);

            int roll = Random.Range(0, 6);
            UiDice[i].GetComponent<Image>().sprite = Faces[roll];
        }
       


    }
}
