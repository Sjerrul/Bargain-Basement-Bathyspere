using System;
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

    private int tokenPosition;

    private int oxygen;
    private int stress;
    private int damage;

    void Start()
    {
        MouseManager.Instance.TokenClicked += OnTokenClick;
        MouseManager.Instance.SquareClicked += OnSquareClick;
        MouseManager.Instance.DieClicked += OnDieClick;

        this.token.VisitSquare += OnTokenVisitsSquare;
        
        this.rules = GetComponent<Rules>();

        Square startingSquare = this.board.GetSquareAtPosition(0);
        this.token.SetCurrentSquare(startingSquare);
        tokenPosition = 0;

        this.oxygen = 10;
        this.stress = 10;
        this.damage = 0;

        InterfaceManager.Instance.SetOxygen(this.oxygen);
        InterfaceManager.Instance.SetStress(this.stress);
        InterfaceManager.Instance.SetDamage(this.damage);
    }

    void OnTokenClick(Token token)
    {
        Debug.Log("Token Clicked");
    }

    private void OnTokenVisitsSquare(Square square)
    {
        Debug.Log("Square Visited: " + square.name);
        if (square.OxygenModifier != 0)
        {
            this.oxygen -= square.OxygenModifier;
            InterfaceManager.Instance.SetOxygen(this.oxygen);
        }

        if (square.StressModifier != 0)
        {
            this.stress -= square.StressModifier;
            InterfaceManager.Instance.SetStress(this.stress);
        }
    }

    void OnSquareClick(Square square)
    {
        Debug.Log("Square Clicked");
        if (square.IsSelected)
        {
            this.board.UnmarkAllSquares();
            var dice = InterfaceManager.Instance.GetDice();
            foreach (var d in dice)
            {
                if (d.IsSelected)
                {
                    GameObject.Destroy(d.gameObject);
                }
            }


            int positionOfSquare = this.board.GetPositionOfSquare(square);
            if (positionOfSquare > this.tokenPosition)
            {
                for (int i = this.tokenPosition + 1; i <= positionOfSquare; i++)
                {
                    Square nextSquare = this.board.GetSquareAtPosition(i);
                    token.AddToPath(nextSquare);
                }
            }
            else
            {
                for (int i = this.tokenPosition - 1; i >= positionOfSquare; i--)
                {
                    Square nextSquare = this.board.GetSquareAtPosition(i);
                    token.AddToPath(nextSquare);
                }
            }

            this.tokenPosition = positionOfSquare;
        }
    }

    void OnDieClick(Die die)
    {
        Debug.Log($"Die {die.Value} clicked");

        var dice = InterfaceManager.Instance.GetDice();
        foreach (var d in dice)
        {
            d.SetSelected(false);
        }

        die.SetSelected(true);
        Square tokenSquare = this.board.GetSquareAtPosition(this.tokenPosition);

        this.board.UnmarkAllSquares();
        Square squareAhead = this.board.GetSquareAfterSteps(tokenSquare, die.Value);
        Square squareBehind = this.board.GetSquareBeforeSteps(tokenSquare, die.Value);
        
        if (squareAhead != null)
        {
            squareAhead.SetMarked(true);
        }

        if (squareBehind != null)
        {
            squareBehind.SetMarked(true);
        }
    }
}
