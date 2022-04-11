using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TouchDetail
{
    int fingerId;
    Touch touch;
    GameObject gameObject;
    Touchable touchable;
    float time;

    public TouchDetail(int fingerId, Touch touch, GameObject gameObject)
    {
        this.fingerId = fingerId;
        this.touch = touch;
        this.gameObject = gameObject;
        this.time = 0f;
        this.touchable = gameObject.GetComponent<Touchable>();
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

    public Touchable GetTouchable()
    {
        return touchable;
    }

    public void updateTouch(Touch touch)
    {
        this.touch = touch;
    }

    public void updateGameObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.touchable = gameObject.GetComponent<Touchable>();

    }
}

public struct Tile
{
    public const int Wall = 0;
    public const int Road = 1;
    public const int Field = 2;
    public const int Finish = 3;

    public const int Hero = 1001;
    public const int Enemy = 1000;
    public const int FinishProp = 1002;
    public const int Heal = 1003;

    public int ground;
    // все беды здесь
    public UnitProp prop;
}

public struct Save
{
    public static Tile[,] tileMap;
}