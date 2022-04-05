using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitDamageable : MonoBehaviour
{
    public float damage = 20;

    public abstract float health { get; set; }
    
    public float healthMax = 100;
    public bool healthChanged = false;
    protected float healthCurrent;
    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Attack();
}
