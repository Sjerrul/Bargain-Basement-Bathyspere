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

    void Start()
    {
        MouseManager.Instance.TokenClicked += OnTokenClick;
        MouseManager.Instance.SquareClicked += OnSquareClick;
        MouseManager.Instance.DieClicked += OnDieClick;

        this.rules = GetComponent<Rules>();

        Square startingSquare = this.board.GetSquareAtPosition(0);
        this.token.SetCurrentSquare(startingSquare);
        tokenPosition = 0;
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
        Debug.Log("Square Clicked");
        if (square.IsMarked)
        {
            this.board.UnmarkAllSquares();

            int positionOfSquare = this.board.GetPositionOfSquare(square);
            if (positionOfSquare > this.tokenPosition)
            {
                for (int i = this.tokenPosition; i <= positionOfSquare; i++)
                {
                    Square nextSquare = this.board.GetSquareAtPosition(i);
                    token.AddToPath(nextSquare);
                }
            }
            else
            {
                for (int i = this.tokenPosition; i >= positionOfSquare; i--)
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
