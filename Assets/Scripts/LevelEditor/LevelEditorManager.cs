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
        Debug.Log($"LevelEditorManager::Serializing board");
        BoardData boardData = BoardSerializer.Serialize(this.Board);

        Debug.Log($"LevelEditorManager::Saving {path}");
        SaveFileHandler.Save(path, boardData);

        Debug.Log($"LevelEditorManager::Saved {path}");
    }

    public void Load()
    {
        string path = Application.dataPath + "/Boards/" + boardName;

        Debug.Log($"LevelEditorManager::Loading {path}");
        BoardData boardData = SaveFileHandler.Load(path);

        Debug.Log($"LevelEditorManager::Deserializing board");
        BoardSerializer.Deserialize(boardData, this.Board, this.squarePrefab);

        Debug.Log($"LevelEditorManager::Loaded {path}");
    }
}
