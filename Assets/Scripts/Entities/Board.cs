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
        Square result = square;
        for (int i = 0; i < steps; i++)
        {
            result = result.NextSquare;
            if (result == null)
            {
                return null;
            }
        }

        return result;
    }

    public Square GetSquareBeforeSteps(Square square, int steps)
    {
        Square result = square;
        for (int i = 0; i < steps; i++)
        {
            result = result.PreviousSquare;
            if (result == null)
            {
                return null;
            }
        }

        return result;
    }
}
