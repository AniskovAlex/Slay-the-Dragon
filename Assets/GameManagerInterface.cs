using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    public void TouchBegan(TouchDetail touch);
    public void TouchMoved(TouchDetail touch);
    public void TouchStationary(TouchDetail touch);
}
