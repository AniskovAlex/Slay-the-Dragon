using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : UnitProp
{
    private void Start()
    {
        
    }
    public override void OnTouch(Vector2 position)
    {
        MapGameManager.Restart();
    }

    public override UnitProp Spawn(int x, int y)
    {
        GameObject finish = Instantiate(gameObject, TilemapBehaviour.IsomToRect(new Vector2(x, y)), Quaternion.identity);
        return finish.GetComponent<Finish>();
    }
}
