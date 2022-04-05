using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : ManagerSingletonBase<InterfaceManager>
{
    public Text StressLabel;
    public Text OxygenLabel;

    public Text DamageLabel;

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

    public void SetStress(int stress)
    {
        this.StressLabel.text = $"{stress}";
    }

    public void SetOxygen(int oxyen)
    {
        this.OxygenLabel.text = $"{oxyen}";
    }

    public void SetDamage(int damage)    
    {
        this.DamageLabel.text = $"{damage}";
    }
    public Die[] GetDice()
    {
        return dicePanel.GetComponentsInChildren<Die>();
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
