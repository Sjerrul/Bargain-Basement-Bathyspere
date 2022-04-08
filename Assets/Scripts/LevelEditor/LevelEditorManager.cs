using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    public GameObject squarePrefab;
    public Board Board;
    public string boardName;

    public void Save()
    {
        string path = Application.dataPath + "/Boards/" + boardName;
        Debug.Log($"LevelEditorManager::Saving {path}");
        BoardFileHandler.Save(path, this.Board);

        Debug.Log($"LevelEditorManager::Saved {path}");
    }

    public void Load()
    {
        string path = Application.dataPath + "/Boards/" + boardName;

        Debug.Log($"LevelEditorManager::Loading {path}");
        BoardFileHandler.Load(path, this.Board, this.squarePrefab);
        Debug.Log($"LevelEditorManager::Loading {path}");
    }
}
