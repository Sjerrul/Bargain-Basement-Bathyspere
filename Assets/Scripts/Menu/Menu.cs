using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneLoader;

public class Menu : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneLoader.LoadScene(Scene.Level);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
