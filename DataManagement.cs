using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour
{
    public static DataManagement datamanagement;

    public int highScore;

    void Awake ()
    {
        if (datamanagement == null)
        {
            DontDestroyOnLoad(gameObject);
            datamanagement = this;
        }
        else if (datamanagement != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData ()
    {
        BinaryFormatter BinForm = new BinaryFormatter(); //laver en bin formatter
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat"); //laver fil
        gameData data = new gameData(); //laver container for data
        data.highScore = highScore;
        BinForm.Serialize(file, data); //serializes
        file.Close();
    }

    public void LoadData ()
    {
        if (File.Exists (Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter BinForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameData data = (gameData)BinForm.Deserialize(file);
            file.Close();
            highScore = data.highScore;
        }
    }

}

[Serializable]
class gameData
{
    public int highScore;

}
