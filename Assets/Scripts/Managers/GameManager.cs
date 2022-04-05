using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerSingletonBase<GameManager>
{
    private Rules rules;
    void Start()
    {
        MouseManager.Instance.TokenClicked += OnTokenClick;
        MouseManager.Instance.SquareClicked += OnSquareClick;
        MouseManager.Instance.DieClicked += OnDieClick;

        this.rules = GetComponent<Rules>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTokenClick(Token token)
    {
        Debug.Log("Token Clicked");
    }

    void OnSquareClick(Square square)
    {
         Debug.Log($"Square {square.name} clicked");
    }

    void OnDieClick(Die die)
    {
        Debug.Log($"Die {die.Value} clicked");
    }
}
