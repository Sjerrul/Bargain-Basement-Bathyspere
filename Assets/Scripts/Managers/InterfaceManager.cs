using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : ManagerSingletonBase<InterfaceManager>
{
    public GameObject dicePanel;

    public Sprite[] DieFaces;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void RollDice()
    {
        foreach (var die in dicePanel.GetComponentsInChildren<Die>())
        {
            GameObject.Destroy(die.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            int roll = UnityEngine.Random.Range(1, 7);
            
            GameObject die = new GameObject($"Die: {roll}", typeof(Image), typeof(Die));
            die.transform.SetParent(this.dicePanel.transform);
            
            die.GetComponent<Image>().sprite = DieFaces[roll - 1];
            die.GetComponent<Die>().Value = roll;
        }
    }
}
