using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    private DiceBox diceBox;

    // Start is called before the first frame update
    void Start()
    {
        this.diceBox = GetComponentInChildren<DiceBox>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollDice(int amount, Action<Die> onClickAction)
    {
        for (int i = 0; i < amount; i++)
        {
            int roll = UnityEngine.Random.Range(1, 7);
            diceBox.AddDie(roll, onClickAction);
        }
    }

    public void ClearDice()
    {
        diceBox.ClearDice();
    }
}
