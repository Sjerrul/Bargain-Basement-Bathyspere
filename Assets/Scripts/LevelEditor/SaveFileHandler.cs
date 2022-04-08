using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public static class SaveFileHandler
{
    public static void Save(string path, BoardData boardData)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));
        }

        if (boardData is null)
        {
            throw new ArgumentNullException(nameof(boardData));
        }

        if (!File.Exists(path))
        {
            File.Create(path);
        }

        var json = JsonUtility.ToJson(boardData);
        File.WriteAllText(path, json);

        Debug.Log("Saved");
    }

    public static BoardData Load(string path)
    {
        if (!File.Exists(path))
        {
            throw new InvalidOperationException($"No save file found on path '{path}'");
        }
            
        string json = File.ReadAllText(path);
        var boardData = JsonUtility.FromJson<BoardData>(json);  
        return boardData;
    }

   
}
