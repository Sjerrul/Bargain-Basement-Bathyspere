using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Token : MonoBehaviour
{
    public event Action<Square> VisitSquare;

    public Square CurrentSquare {get; private set; }

    public float smoothTime = 0.15F;


    private Vector3 velocity = Vector3.zero;
    private Queue<Square> path = new Queue<Square>();

    private bool inTransit = false;

    void Update()
    {
        if (this.CurrentSquare == null)
        {
            return;
        }

        if (Vector3.Distance(this.transform.position, this.CurrentSquare.transform.position) < 0.01f)
        {
            if (inTransit)
            {
                inTransit = false;
                if (VisitSquare != null)
                {
                    VisitSquare(this.CurrentSquare);
                }
            }

            if (path.Count <= 0)
            {
                return;
            }

            this.CurrentSquare = path.Dequeue();
        }

        inTransit = true;
        transform.position = Vector3.SmoothDamp(this.transform.position, this.CurrentSquare.transform.position, ref this.velocity, this.smoothTime);
    }   

    public void SetCurrentSquare(Square currentSquare)
    {
        Debug.Log("Token::Set to square: " + currentSquare.name);
        this.CurrentSquare = currentSquare;
        this.transform.position = currentSquare.transform.position;

        Debug.Log($"Token::Moved to ({currentSquare.transform.position.x}, {currentSquare.transform.position.y}, {currentSquare.transform.position.z})");
    }

    public void AddToPath(Square square)
    {
        this.path.Enqueue(square);
    }
}
