using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeft : Touchable
{

    public GameObject _managerBody;
    TilemapBehaviour _map;
    MapGameManager _manager;

    private void Awake()
    {
        _manager = _managerBody.GetComponent<MapGameManager>();
        if (_map == null) _map = GameObject.Find("Map Generater").GetComponent<TilemapBehaviour>();
        if (_manager ==null) _manager = GameObject.Find("Game Manager").GetComponent<MapGameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_manager);
    }

    public override void Touched()
    {
        if (_manager.GetTimeDelay() <= 0)
            if (_map.movePlayer(new Vector2(-1, 1), Vector2.left))
                _manager.SetTimeDelayDiagoanl();
    }

    public override void Touching()
    {
        if (_manager.GetTimeDelay() <= 0)
            if (_map.movePlayer(new Vector2(-1, 1), Vector2.left))
                _manager.SetTimeDelayDiagoanl();
    }
}
