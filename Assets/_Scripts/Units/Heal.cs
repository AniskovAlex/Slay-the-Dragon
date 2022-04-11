using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : UnitProp
{
    public GameObject deactivatedBody;
    int healValue = 35;

    public override void OnTouch(Vector2 position)
    {
        _manager = GameObject.Find("Game Manager").GetComponent<MapGameManager>();
        _manager.ChangeHealth(healValue);
        DisableObject(position);
    }

    void DisableObject(Vector2 position)
    {
        TilemapBehaviour _map = GameObject.Find("Map Generater").GetComponent<TilemapBehaviour>();
        _map.SwapProp(deactivatedBody.GetComponent<UnitProp>(), position);
    }
}
