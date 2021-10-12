using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TouchDetail
{
    int fingerId;
    Touch touch;
    GameObject gameObject;
    float time;

    public TouchDetail(int fingerId, Touch touch, GameObject gameObject)
    {
        this.fingerId = fingerId;
        this.touch = touch;
        this.gameObject = gameObject;
        this.time = 0f;
    }

    public void timePlus(float deltaTime)
    {
        time += deltaTime;
    }

    public Touch getTouch()
    {
        return touch;
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public int getId()
    {
        return fingerId;
    }

    public void updateTouch(Touch touch)
    {
        this.touch = touch;
    }
}
