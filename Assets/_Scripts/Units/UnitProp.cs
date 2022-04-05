using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitProp : MonoBehaviour
{
    public Vector2 isomPosition;

    public virtual void OnTouch() { }

    public virtual UnitProp Spawn(int x,int y) { return this; }
}
