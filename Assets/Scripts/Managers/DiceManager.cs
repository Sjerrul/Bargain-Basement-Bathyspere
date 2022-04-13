using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : ManagerSingletonBase<DiceManager>
{
    public GameObject dicePanel;

    public Sprite[] DieFaces;

    public Die[] GetDice()
    {
        return dicePanel.GetComponentsInChildren<Die>();
    }

    public bool AreDiceAvailable()
    {
        return dicePanel.GetComponentsInChildren<Die>().Length != 0;
    }
    
    public void RollDice()
    {
        var currentAvailableDice = dicePanel.GetComponentsInChildren<Die>();
        foreach (var die in currentAvailableDice)
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
