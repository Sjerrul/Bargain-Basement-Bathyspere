using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public Square StartingSquare {get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        var squares = this.GetComponentsInChildren<Square>();
        this.StartingSquare = squares.Single(x => x.PreviousSquare == null);
    }

    // Update is called once per frame
    void Update()
    {
        //.DamageLabel.text = $"{this.Damage}";
        //this.StressLabel.text = $"{this.Stress}";
    }
}
