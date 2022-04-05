using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : Touchable
{
    public BattleGameManager _gameManager;
    public override void Touched()
    {
        Debug.Log("SSSSs");
        _gameManager.PreloadSceen();
    }
}
