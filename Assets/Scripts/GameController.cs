using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Board Board;

    public Token Token;

    public UiController UiController;

    private Rules rules;

    // Start is called before the first frame update
    void Start()
    {
        this.Token.MouseClickEvent += OnTokenClick;

        foreach (Square square in Board.GetComponentsInChildren<Square>())
        {
            Debug.Log("Found");
            square.MouseClickEvent += OnSquareClick;
        }

        this.rules = GetComponent<Rules>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTokenClick()
    {
        Debug.Log("Token Clicked");
    }

    void OnSquareClick(Square square)
    {
         Debug.Log($"Square {square.name} clicked");
    }

    void OnDiceClick(Die die)
    {
        Debug.Log($"Die {die.Value} clicked");
    }
    
    public void RollDice()
    {
        Debug.Log("Roll Dice");
        this.UiController.ClearDice();
        this.UiController.RollDice(this.rules.NumberOfDice, OnDiceClick);
    }
}
