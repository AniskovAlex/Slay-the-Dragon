using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitProp : MonoBehaviour
{
    public Vector2 isomPosition;
    protected MapGameManager _manager;

    private void Start()
    {
        
    }

    private void Awake()
    {
        
    }
    public virtual void OnTouch(Vector2 position) { }

    public virtual UnitProp Spawn(int x,int y) {
        GameObject _object = Instantiate(gameObject, TilemapBehaviour.IsomToRect(new Vector2(x, y)), Quaternion.identity);
        _object.GetComponent<UnitProp>().isomPosition = new Vector2(x, y);
        Debug.Log("ffffffff" + isomPosition);
        return _object.GetComponent<UnitProp>();
    }
}
