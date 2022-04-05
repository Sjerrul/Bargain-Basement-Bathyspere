using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{

    public Material squareMaterial;
    public Material selectedSquareMaterial;
    
    public Text DamageLabel;
    public Text StressLabel;

    public Token Token;

    private int Damage;
    private int Stress;

    private int Oxygen;

    private Square[] squares;

    // Start is called before the first frame update
    void Start()
    {
        squares = this.GetComponentsInChildren<Square>();
    }

    // Update is called once per frame
    void Update()
    {
        //.DamageLabel.text = $"{this.Damage}";
        //this.StressLabel.text = $"{this.Stress}";
    }

    public Square[] GetSquares()
    {
        return squares;
    }
}
