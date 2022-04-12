using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneLoader;

public class GameManager : ManagerSingletonBase<GameManager>
{
    private Rules rules;
    private int tokenPosition;
    private int oxygen;
    private int stress;
    private int damage;


    public string BoardToLoad;
    public Board Board;

    public Token Token;

    public GameObject squarePrefab;
    public GameObject oxygenParticleSystem;
    public GameObject stressParticleSystem;
    public GameObject damageParticleSystem;
    void Awake()
    {
        Debug.Log("GameManager::Loading level " + this.BoardToLoad);
        string path = Application.dataPath + "/Boards/" + this.BoardToLoad;

        Debug.Log($"GameManager::Loading {path}");
        BoardData boardData = SaveFileHandler.Load(path);

        Debug.Log($"GameManager::Deserializing board");
        BoardSerializer.Deserialize(boardData, this.Board, this.squarePrefab);
    }

    void Start()
    {
        Debug.Log("GameManager::Adding event listeners");
        InputManager.Instance.TokenClicked += OnTokenClick;
        InputManager.Instance.SquareClicked += OnSquareClick;
        InputManager.Instance.DieClicked += OnDieClick;

        this.Token.PassSquare += OnTokenPassesSquare;
        this.Token.LandOnSquare += OnTokenLandsOnSquare;
        
        this.rules = GetComponent<Rules>();

        Debug.Log("GameManager::Setting up board");
        Square startingSquare = this.Board.GetSquareAtPosition(0);
        this.Token.SetCurrentSquare(startingSquare);
        tokenPosition = 0;

        this.oxygen = 10;
        this.stress = 10;
        this.damage = 0;

        Debug.Log("GameManager::Setting up interface");
        InterfaceManager.Instance.SetOxygen(this.oxygen);
        InterfaceManager.Instance.SetStress(this.stress);
        InterfaceManager.Instance.SetDamage(this.damage);
    }

    void OnTokenClick(Token token)
    {
        Debug.Log("GameManager::Token Clicked");
        Instantiate(stressParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
    }

    private void OnTokenPassesSquare(Square square)
    {
        Debug.Log("GameManager::Square Visited: " + square.name);
        if (square.OxygenModifier != 0)
        {
            this.oxygen -= square.OxygenModifier;
            InterfaceManager.Instance.SetOxygen(this.oxygen);
            Instantiate(oxygenParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
        }

        if (square.StressModifier != 0)
        {
            this.stress -= square.StressModifier;
            InterfaceManager.Instance.SetStress(this.stress);
             Instantiate(stressParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
        }
    }

    private void OnTokenLandsOnSquare(Square square)
    {
        Debug.Log("GameManager::Square Landed: " + square.name);
        if (square.SquareType == SquareType.Start)
        {
            SceneLoader.LoadScene(Scene.LevelEndMenu);
        }


        square.SetMarked(true);
    }

    void OnSquareClick(Square square)
    {
        Debug.Log("GameManager::Square Clicked: " + square.name);
        if (square.IsSelected)
        {
            this.Board.UnselectAllSquares();
            var dice = InterfaceManager.Instance.GetDice();
            foreach (var d in dice)
            {
                if (d.IsSelected)
                {
                    GameObject.Destroy(d.gameObject);
                }
            }


            int positionOfSquare = this.Board.GetPositionOfSquare(square);
            if (positionOfSquare > this.tokenPosition)
            {
                for (int i = this.tokenPosition + 1; i <= positionOfSquare; i++)
                {
                    Square nextSquare = this.Board.GetSquareAtPosition(i);
                    this.Token.AddToPath(nextSquare);
                }
            }
            else
            {
                for (int i = this.tokenPosition - 1; i >= positionOfSquare; i--)
                {
                    Square nextSquare = this.Board.GetSquareAtPosition(i);
                    this.Token.AddToPath(nextSquare);
                }
            }

            this.tokenPosition = positionOfSquare;
        }
    }

    void OnDieClick(Die die)
    {
        Debug.Log($"GameManager::Die {die.Value} clicked");

        var dice = InterfaceManager.Instance.GetDice();
        foreach (var d in dice)
        {
            d.SetSelected(false);
        }

        die.SetSelected(true);
        Square tokenSquare = this.Board.GetSquareAtPosition(this.tokenPosition);

        this.Board.UnselectAllSquares();
        Square squareAhead = this.Board.GetSquareAfterSteps(tokenSquare, die.Value);
        Square squareBehind = this.Board.GetSquareBeforeSteps(tokenSquare, die.Value);
        
        if (squareAhead != null)
        {
            squareAhead.SetSelected(true);
        }

        if (squareBehind != null)
        {
            squareBehind.SetSelected(true);
        }
        else
        {
            if (tokenPosition == 0)
            {
                return;
            }

            Debug.Log("NoSquareBehind");
            // Can only happen when moving of the top of the board, so activate the first square
            var firstSquare = this.Board.GetSquareAtPosition(0);

            Debug.Log(firstSquare.name);
            firstSquare.SetSelected(true);
        }
    }
}
