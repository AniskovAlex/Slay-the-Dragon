using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHero : UnitProp
{
    //public GameObject _hero;
    public TilemapBehaviour _map;
    float health;
    float stamina;
    public GameObject _hero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override UnitProp Spawn(int x, int y)
    {
        GameObject hero = Instantiate(_hero, TilemapBehaviour.IsomToRect(new Vector2(x, y)), Quaternion.identity);
        hero.GetComponent<MapHero>().isomPosition = new Vector2(x, y);
        hero.GetComponent<MapHero>()._map = GameObject.Find("Map Generater").GetComponent<TilemapBehaviour>();
        hero.GetComponent<MapHero>().SetControl();
        hero.GetComponent<MapHero>()._map.SetHero(hero.GetComponent<MapHero>());
        return hero.GetComponent<MapHero>();
    }

    public bool MovePlayer(Vector2 mapDirection, Vector2 playerDirection)
    {
        return _map.MovePlayer(mapDirection, playerDirection, this);
    }

    void SetControl()
    {
        GameObject.Find("down button").GetComponent<ButtonDown>()._hero = this;
        GameObject.Find("down right button").GetComponent<ButtonDownRight>()._hero = this;
        GameObject.Find("down left button").GetComponent<ButtonDownLeft>()._hero = this;
        GameObject.Find("up button").GetComponent<ButtonUp>()._hero = this;
        GameObject.Find("up right button").GetComponent<ButtonUpRight>()._hero = this;
        GameObject.Find("up left button").GetComponent<ButtonUpLeft>()._hero = this;
        GameObject.Find("left button").GetComponent<ButtonLeft>()._hero = this;
        GameObject.Find("right button").GetComponent<ButtonRight>()._hero = this;
    }
}
