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
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/Boards/" + boardName;

        FileStream stream = new FileStream(path, FileMode.Create);

        var array = new List<SquareData>();
        foreach (var square in Board.GetComponentsInChildren<Square>())
        {
            SquareData squareData = new SquareData(square);
            array.Add(squareData);
            
        }
        formatter.Serialize(stream, array);
        stream.Close();

        Debug.Log("Saved");
    }

    public void Load()
    {
        foreach (var child in this.Board.GetComponentsInChildren<Square>())
        {
            GameObject.Destroy(child.gameObject);
        }

        List<SquareData> s = LoadSquare();
        foreach (var data in s)
        {
            var createdSquare = GameObject.Instantiate(squarePrefab);
            createdSquare.transform.SetParent(this.Board.transform);
            createdSquare.name = data.Name; 

            var square = createdSquare.GetComponent<Square>();
            square.transform.position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
            square.transform.rotation = new Quaternion(data.Rotation[0], data.Rotation[1], data.Rotation[2], data.Rotation[3]);
            square.OxygenModifier = data.OxygenModifier;
            square.StressModifier = data.StressModifier;


        }
    }

    public List<SquareData> LoadSquare()
    {
        string path = Application.dataPath + "/Boards/" + boardName;
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
