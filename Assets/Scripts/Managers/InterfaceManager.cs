using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : ManagerSingletonBase<InterfaceManager>
{
    public event Action RollDiceClick;
    public event Action RerollDiceClick;

    public Text StressLabel;
    public Text OxygenLabel;

    public Text DamageLabel;

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

    public void RollDiceButtonClick()
    {
        if (RollDiceClick != null)
        {
            RollDiceClick();
        }
    }
}
