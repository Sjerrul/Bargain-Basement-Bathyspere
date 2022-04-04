using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{

    public Material squareMaterial;
    public Material selectedSquareMaterial;
    
    public Text DamageLabel;
    public Text StressLabel;

    public DiceBox DiceBox;
    public Token Token;

    private int Damage;
    private int Stress;

    private int Oxygen;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.DamageLabel.text = $"{this.Damage}";
        this.StressLabel.text = $"{this.Stress}";
    }

    void DiceClick(Die die)
    {
        Debug.Log("Clicked die" + die.Value);

        Square square = Token.CurrentSquare;
        for (int i = 0; i < die.Value; i++)
        {
            square = square.NextSquare;
        }
        
        square.GetComponentInChildren<MeshRenderer>().material = selectedSquareMaterial;
    }
}
