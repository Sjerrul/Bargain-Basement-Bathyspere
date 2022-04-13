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
