using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : ManagerSingletonBase<MouseManager>
{
    public event Action<Token> TokenClicked;
    public event Action<Square> SquareClicked;
    public event Action<Die> DieClicked;
 
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (CheckUIElementClicked())
            {
                return;
            }   

            if (CheckBoardElementClicked())
            {
                return;
            }
        }
    }

    private bool CheckUIElementClicked()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {          
            PointerEventData pointerData = new PointerEventData(EventSystem.current) {
                pointerId = -1,
            };

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            foreach (var result in results)
            {
                if (result.gameObject.GetComponent<Die>() != null)
                {
                    if (DieClicked != null)
                    {
                        DieClicked(result.gameObject.GetComponent<Die>());
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool CheckBoardElementClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.GetComponent<Token>() != null)
            {
                if (TokenClicked != null)
                {
                    TokenClicked(hit.collider.GetComponent<Token>());
                    return true;
                }
            }

            if (hit.collider.GetComponent<Square>() != null)
            {
                if (SquareClicked != null)
                {
                    SquareClicked(hit.collider.GetComponent<Square>());
                    return true;
                }
            }
        }

        return false;
    }
}
