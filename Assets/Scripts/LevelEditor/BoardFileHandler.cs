using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public static class BoardFileHandler
{
    public static void Save(string path, Board boardToSave)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));
        }

        if (boardToSave is null)
        {
            throw new ArgumentNullException(nameof(boardToSave));
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            var array = new List<SquareData>();
            foreach (var square in boardToSave.GetComponentsInChildren<Square>())
            {
                SquareData squareData = new SquareData(square);
                array.Add(squareData);
                
            }
            formatter.Serialize(stream, array);         
        }

        string path2 = path + "_json";
        using (FileStream stream = new FileStream(path2, FileMode.Create))
        {
            var array = new List<SquareData>();
            foreach (var square in boardToSave.GetComponentsInChildren<Square>())
            {
                SquareData squareData = new SquareData(square);
                array.Add(squareData);
                var  j= JsonUtility.ToJson(squareData);  
                Debug.Log(j);
            }

            BoardData data = new BoardData
            {
                Squares = array
            };

            var json = JsonUtility.ToJson(data);  
            Debug.Log(json);
            byte[] info = new UTF8Encoding(true).GetBytes(json);
       
            stream.Write(info, 0, info.Length);
        }

        Debug.Log("Saved");
    }

    public static void Load(string path, Board boardToLoadInto, GameObject squarePrefab)
    {
        foreach (var square in boardToLoadInto.GetComponentsInChildren<Square>())
        {
            GameObject.DestroyImmediate(square.gameObject);
        }

        List<SquareData> s = LoadSquareDataFromFile(path);
        foreach (var data in s)
        {
            var createdSquare = GameObject.Instantiate(squarePrefab);
            createdSquare.transform.SetParent(boardToLoadInto.transform);
            createdSquare.name = data.Name; 

            var square = createdSquare.GetComponent<Square>();
            square.transform.position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
            square.transform.rotation = new Quaternion(data.Rotation[0], data.Rotation[1], data.Rotation[2], data.Rotation[3]);
            square.OxygenModifier = data.OxygenModifier;
            square.StressModifier = data.StressModifier;
        }
    }

    private static List<SquareData> LoadSquareDataFromFile(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<SquareData> data = formatter.Deserialize(stream) as List<SquareData>;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Savefile not found");
        }

        return null;
    }
}
