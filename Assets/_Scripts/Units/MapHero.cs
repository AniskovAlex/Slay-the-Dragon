using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHero : UnitProp
{
    //public GameObject _hero;
    
    private float health;
    private float stamina;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Spawn(int x, int y)
    {
        gameObject.transform.position = TilemapBehaviour.IsomToRect(new Vector2(x, y));
        Debug.Log("DDDDDDDDDDDDDDD");
        isomPosition = new Vector2(x, y);
    }
}
