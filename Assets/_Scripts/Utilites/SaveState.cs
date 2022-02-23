using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState : MonoBehaviour
{
    Hero hero;
    Tile[,] map;

    public SaveState(Hero hero, Tile[,] map)
    {
        this.hero = hero;
        this.map = map;
    }

    public void SaveMap(Tile[,] map)
    {
        this.map = map;
    }
    public void SaveHero(Hero hero)
    {
        this.hero = hero;
    }

    public Tile[,] LoadMap()
    {
        return map;
    }

    public Hero LoadHero()
    {
        return hero;
    }
}
