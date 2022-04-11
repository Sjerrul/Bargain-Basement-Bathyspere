using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        Menu = 0,
        Level = 1,

        LevelEndMenu = 2
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene, LoadSceneMode.Single);
        
    }
}
