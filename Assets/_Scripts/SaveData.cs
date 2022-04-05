using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveData : ScriptableObject
{
    static SaveData instance;

    Tile[,] map;
    int hp;
    public void SaveMap(Tile[,] map)
    {
        this.map = map;
    }

    public Tile[,] LoadMap()
    {
        return map;
    }

    public void SaveHealth(int hp)
    {
        this.hp = hp;
    }

    public int LoadHealth()
    {
        return hp;
    }

    public void ResetData()
    {
        map = null;
        hp = 0;
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
