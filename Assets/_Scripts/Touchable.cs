using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Touchable : MonoBehaviour
{
    public virtual void Touched() { }
    public virtual void Touching() { }
}
