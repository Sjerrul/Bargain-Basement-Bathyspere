using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Board : MonoBehaviour
{
    private Square[] squares;

    void Start()
    {
        Debug.Log($"Board::Loading my squares in memory");
        this.squares = this.GetComponentsInChildren<Square>();
        Debug.Log($"Board::Squares found: {string.Join(", ", this.squares.Select(x => x.name))}");
    }


    public Square GetSquareAtPosition(int position)
    {
        Square square = this.squares[position];
        Debug.Log($"Board::Square at position {position} is {square.name}");
        return square;
    }

    public int GetPositionOfSquare(Square square)
    {
        for (int i = 0; i < squares.Length; i++)
        {
            if (squares[i] == square)
            {
                return i;
            }
        }

        return -1;
    }

    public void UnmarkAllSquares()
    {
        for (int i = 0; i < squares.Length; i++)
        {
            squares[i].SetMarked(false);
        }
    }

    public Square GetSquareAfterSteps(Square square, int steps)
    {
        var position = this.GetPositionOfSquare(square);
        var targetPosition = position + steps;
        if (targetPosition >= this.squares.Length)
        {
            return null;
        }

        return this.squares[targetPosition];
    }

    public Square GetSquareBeforeSteps(Square square, int steps)
    {
        var position = this.GetPositionOfSquare(square);

        var targetPosition = position - steps;
        if (targetPosition < 0)
        {
            return null;
        }
        
        return this.squares[position - steps];
    }
}
