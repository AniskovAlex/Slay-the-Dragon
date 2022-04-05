using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDown : Touchable
{

    public GameObject _managerBody;
    public MapHero _hero;
    MapGameManager _manager;

    // Start is called before the first frame update
    void Start()
    {
        _manager = _managerBody.GetComponent<MapGameManager>();
        //if (_hero == null) _hero = GameObject.Find("Hero(Clone)").GetComponent<MapHero>();
        if (_manager == null) _manager = GameObject.Find("Game Manager").GetComponent<MapGameManager>();
    }

    public override void Touched()
    {
        //Debug.Log("S");
        if (_manager.GetTimeDelay() <= 0)
            if (_hero.MovePlayer(new Vector2(-1, -1), Vector2.down * 0.5f))
                _manager.SetTimeDelayDiagoanl();
    }

    public override void Touching()
    {
        if (_manager.GetTimeDelay() <= 0)
            if (_hero.MovePlayer(new Vector2(-1, -1), Vector2.down * 0.5f))
                _manager.SetTimeDelayDiagoanl();
    }

}
