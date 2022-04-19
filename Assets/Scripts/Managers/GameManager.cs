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

        PopupManager.Instance.PopupOxygenClick += OnPopupOxygenClick;
        PopupManager.Instance.PopupDamageClick += OnPopupDamageClick;
        PopupManager.Instance.PopupStressClick += OnPopupStressClick;

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

        DiceManager.Instance.RollDice();
    }

    void OnPopupOxygenClick(Square square)
    {
        Debug.Log("GameManager::OnPopupOxygenClick");
        PopupManager.Instance.Hide();
        Time.timeScale = 1;        
        
        DecrementOxygen(square.OxygenModifier);
        square.OxygenModifier = 0;
    }

    void OnPopupDamageClick(Square square)
    {
        Debug.Log("GameManager::OnPopupDamageClick");
        PopupManager.Instance.Hide();
        Time.timeScale = 1;        
        
        DecrementDamage(square.DamageModifier);
        square.DamageModifier = 0;
    }

    void OnPopupStressClick(Square square)
    {
        Debug.Log("GameManager::OnPopupStressClick");
        PopupManager.Instance.Hide();
        Time.timeScale = 1;    

        DecrementDamage(square.StressModifier);
        square.StressModifier = 0;
    }

    void OnTokenClick(Token token)
    {
        Debug.Log("GameManager::Token Clicked");
    }

    private void OnTokenPassesSquare(Square square)
    {
        Debug.Log("GameManager::Square Visited: " + square.name);
        if (square.IsChoice())
        {
            Time.timeScale = 0;
            PopupManager.Instance.Show(square);
            
            return;
        }

        if (square.OxygenModifier != 0 && !square.IsMarked)
        {
            DecrementOxygen( square.OxygenModifier);
            square.OxygenModifier = 0;
        }

        if (square.StressModifier != 0 && !square.IsMarked)
        {
            DecrementStress( square.StressModifier);
            square.StressModifier = 0;
        }

        if (square.DamageModifier != 0 && !square.IsMarked)
        {
            DecrementDamage( square.DamageModifier);
            square.DamageModifier = 0;
        }

        if (square.IsDepthZone)
        {
            this.passedDepthLine = true;
            this.indexOfDepthLinePassing = this.Board.GetPositionOfSquare(square);
        }
    }

    private void DecrementOxygen(int amount)
    {
        this.oxygen -= amount;
        InterfaceManager.Instance.SetOxygen(this.oxygen);
        Instantiate(oxygenParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
    }

    private void DecrementStress(int amount)
    {
        this.stress -= amount;
        InterfaceManager.Instance.SetStress(this.stress);
        Instantiate(stressParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
    }

    private void DecrementDamage(int amount)
    {
        this.damage -= amount;
        InterfaceManager.Instance.SetDamage(this.damage);
        Instantiate(damageParticleSystem, this.Token.transform.position, this.Token.transform.rotation);
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
            DecrementStress(1);
        }

        if (square.IsDepthZone)
        {
            DecrementStress(1);
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

            DecrementStress(squares);
            passedDepthLine = false;
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
        DecrementOxygen(1);
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
