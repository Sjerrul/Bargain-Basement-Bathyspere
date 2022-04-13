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

    private bool gamePaused;

    private bool passedDepthLine;
    private int indexOfDepthLinePassing;

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
        string path = Application.streamingAssetsPath + "/Boards/" + this.BoardToLoad;

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
        InputManager.Instance.MenuKeyPressed += OnMenuKeyPressed;
        InputManager.Instance.PauseKeyPressed += OnPauseKeyPressed;
        InterfaceManager.Instance.RollDiceClick += OnRollClick;
        InterfaceManager.Instance.RerollDiceClick += OnRerollClick;

        this.Token.PassSquare += OnTokenPassesSquare;
        this.Token.LandOnSquare += OnTokenLandsOnSquare;
        
        this.rules = GetComponent<Rules>();

        Debug.Log("GameManager::Setting up board");
        Square startingSquare = this.Board.GetSquareAtPosition(0);
        this.Token.SetCurrentSquare(startingSquare);
        tokenPosition = 0;

        this.oxygen = 10;
        this.stress = 10;
        this.damage = 10;

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
        if (square.OxygenModifier != 0 && !square.IsMarked)
        {
            this.oxygen -= square.OxygenModifier;
            InterfaceManager.Instance.SetOxygen(this.oxygen);
            Instantiate(oxygenParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
        }

        if (square.StressModifier != 0 && !square.IsMarked)
        {
            this.stress -= square.StressModifier;
            InterfaceManager.Instance.SetStress(this.stress);
            Instantiate(stressParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
        }

        if (square.DamageModifier != 0 && !square.IsMarked)
        {
            this.damage -= square.DamageModifier;
            InterfaceManager.Instance.SetDamage(this.damage);
            Instantiate(damageParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
        }

        if (square.IsDepthZone)
        {
            this.passedDepthLine = true;
            this.indexOfDepthLinePassing = this.Board.GetPositionOfSquare(square);
        }
    }

    private void OnTokenLandsOnSquare(Square square)
    {
        Debug.Log("GameManager::Square Landed: " + square.name);
        if (square.SquareType == SquareType.Start)
        {
            SceneLoader.LoadScene(Scene.LevelEndMenu);
        }

        if (square.IsMarked)
        {
            this.stress -= square.StressModifier;
            InterfaceManager.Instance.SetStress(this.stress);
            Instantiate(stressParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
        }

        if (square.IsDepthZone)
        {
            this.stress -= 1;
            InterfaceManager.Instance.SetStress(this.stress);
            Instantiate(stressParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
        }

        if (passedDepthLine)
        {
             int squares = 0;
            if (this.tokenPosition > this.indexOfDepthLinePassing)
            {
                    // GOing down
                squares = this.tokenPosition - this.indexOfDepthLinePassing + 1;
            } else
            {
                squares = this.indexOfDepthLinePassing - this.tokenPosition;
            }

            this.stress -= squares;
            InterfaceManager.Instance.SetStress(this.stress);
            Instantiate(stressParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
        }

        square.SetMarked(true);
    }

    void OnSquareClick(Square square)
    {
        Debug.Log("GameManager::Square Clicked: " + square.name);
        if (square.IsSelected)
        {
            this.Board.UnselectAllSquares();
            var dice = DiceManager.Instance.GetDice();
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

    void OnMenuKeyPressed()
    {
        Debug.Log($"GameManager::Menu key pressed");
        SceneLoader.LoadScene(Scene.Menu);
    }

    void OnPauseKeyPressed()
    {
        Debug.Log($"GameManager::Pause key pressed");
        Time.timeScale = gamePaused ? 1 : 0;
        gamePaused = !gamePaused;
    }

    void OnRollClick()
    {
        if (DiceManager.Instance.AreDiceAvailable())
        {
            return;
        }

        DiceManager.Instance.RollDice();
    }

    void OnRerollClick()
    {
        this.oxygen -= 1;
        InterfaceManager.Instance.SetOxygen(this.oxygen);
        Instantiate(oxygenParticleSystem, this.Token.transform.position, this.Token.transform.rotation);

        DiceManager.Instance.RollDice();
    }

    void OnDieClick(Die die)
    {
        Debug.Log($"GameManager::Die {die.Value} clicked");
        if (die.IsSelected)
        {
            die.SetSelected(false);
            this.Board.UnselectAllSquares();
            return;
        }

        var dice = DiceManager.Instance.GetDice();
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

            // Can only happen when moving of the top of the board, so activate the first square
            var firstSquare = this.Board.GetSquareAtPosition(0);

            Debug.Log(firstSquare.name);
            firstSquare.SetSelected(true);
        }
    }
}
