using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDefence : Touchable
{

    public override void Touched()
    {
        _hero.Defence();
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
