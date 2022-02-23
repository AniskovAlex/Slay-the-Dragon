using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveData : ScriptableObject
{
    static SaveData instance;

    Tile[,] map;
    public void SaveMap(Tile[,] map)
    {
        this.map = map;
    }

    public Tile[,] LoadMap()
    {
        return map;
    }

    public static SaveData GetSaveData()
    {
        if (!instance)
        {
            instance = Resources.Load("Save Data") as SaveData;
        }

        return instance;
    }
}
