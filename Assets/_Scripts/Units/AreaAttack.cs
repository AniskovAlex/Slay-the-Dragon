using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : Touchable
{

    public override void Touched()
    {
        _hero.Attack();
    }

    public override void Touching()
    {
        
    }

    Hero _hero;
    // Start is called before the first frame update
    void Start()
    {
        _hero = GameObject.Find("Hero").GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
