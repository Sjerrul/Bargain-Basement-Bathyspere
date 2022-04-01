using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public Text DamageLabel;
    public Text StressLabel;

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
}
