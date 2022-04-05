using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerSingletonBase<GameManager>
{
    private Rules rules;
    private float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    public Board board;

    public Token token;

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
        Square target = this.board.StartingSquare.NextSquare.NextSquare;
        token.SetCurrentSquare(target);
    }

    void OnSquareClick(Square square)
    {
         Debug.Log($"Square {square.name} clicked");
    }

    void OnDieClick(Die die)
    {
        Debug.Log($"Die {die.Value} clicked");
        Square targetSquare = this.token.CurrentSquare;

        for (int i = 0; i < die.Value; i++)
        {
            targetSquare = targetSquare.NextSquare;
        }

    }
}
