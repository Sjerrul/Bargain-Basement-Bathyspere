using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    private Square[] squares;

    void Start()
    {
        squares = this.GetComponentsInChildren<Square>();
    }

    public Square GetSquareAtPosition(int position)
    {
        return this.squares[position];
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
