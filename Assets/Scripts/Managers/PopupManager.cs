using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : ManagerSingletonBase<PopupManager>
{
    public event Action<Square> PopupOxygenClick;
    public event Action<Square> PopupDamageClick;
    public event Action<Square> PopupStressClick;

    public GameObject PopupPanel;

    private Square triggeredSquare;

    public void Show(Square square)
    {
        this.PopupPanel.SetActive(true);
        this.triggeredSquare = square;

        if (this.triggeredSquare.DamageModifier == 0)
        {
            PopupPanel.transform.Find("Canvas/Panel/Damage").gameObject.SetActive(false);
        }

        if (this.triggeredSquare.OxygenModifier == 0)
        {
            PopupPanel.transform.Find("Canvas/Panel/Oxygen").gameObject.SetActive(false);
        }

        if (this.triggeredSquare.StressModifier == 0)
        {
            PopupPanel.transform.Find("Canvas/Panel/Stress").gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        this.PopupPanel.SetActive(false);
    }

    public void SelectOxygen()
    {
        if (PopupOxygenClick != null)
        {
            PopupOxygenClick(this.triggeredSquare);
        }
    }

    public void SelectDamage()
    {
        if (PopupDamageClick != null)
        {
            PopupDamageClick(this.triggeredSquare);
        }
    }

    public void SelectStress()
    {
        if (PopupStressClick != null)
        {
            PopupStressClick(this.triggeredSquare);
        }
    }

}
