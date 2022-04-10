using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BoardSerializer
{
    public static BoardData Serialize(Board board)
    {
        if (board is null)
        {
            throw new ArgumentNullException(nameof(board));
        }

        var serializedSquares = new List<SquareData>();
        foreach (var square in board.GetComponentsInChildren<Square>())
        {
            SquareData squareData = new SquareData(square);
            serializedSquares.Add(squareData);
        }

        BoardData boardData = new BoardData
        {
            Squares = serializedSquares
        };

        return boardData;
    }

    public static void Deserialize(BoardData boardData, Board board, GameObject squarePrefab)
    {
        foreach (var square in board.GetComponentsInChildren<Square>())
        {
            GameObject.DestroyImmediate(square.gameObject);
        }

        var generatedSquares = new List<Square>();
        foreach (var squareData in boardData.Squares)
        {
            var createdSquare = GameObject.Instantiate(squarePrefab);
            createdSquare.transform.SetParent(board.transform);
            createdSquare.name = squareData.Name; 

            var square = createdSquare.GetComponent<Square>();
            square.transform.position = new Vector3(squareData.Position[0], squareData.Position[1], squareData.Position[2]);
            square.transform.rotation = new Quaternion(squareData.Rotation[0], squareData.Rotation[1], squareData.Rotation[2], squareData.Rotation[3]);
            square.OxygenModifier = squareData.OxygenModifier;
            square.StressModifier = squareData.StressModifier;
            square.DamageModifier = squareData.DamageModifier;
            square.Reference = squareData.Reference;

            generatedSquares.Add(square);
        }

        foreach (var squareData in boardData.Squares)
        {
            var createdSquare = generatedSquares.Single(x => x.Reference == squareData.Reference);
            if (!string.IsNullOrWhiteSpace(squareData.NextSquare))
            {
                var nextSquare = generatedSquares.Single(x => x.Reference == squareData.NextSquare);
                createdSquare.NextSquare = nextSquare;
            }

            if (!string.IsNullOrWhiteSpace(squareData.PreviousSquare))
            {
                var previousSquare = generatedSquares.Single(x => x.Reference == squareData.PreviousSquare);
                createdSquare.PreviousSquare = previousSquare;
            }
        }
    }

}
