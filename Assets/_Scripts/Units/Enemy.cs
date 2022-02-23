using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitDamageable
{

    public float timeToAttackMin;
    public float timeToAttackRange;

    public delegate void attackEventDel(UnitDamageable attacker);
    event attackEventDel _attackEvent;

    public delegate void winEventDel();
    event winEventDel _winEvent;

    GameObject healthBar;

    public override float health
    {
        get => healthCurrent;
        set
        {
            healthCurrent = Mathf.Clamp(value, 0, healthMax);
            healthChanged = true;
            if (healthCurrent == 0)
                _winEvent();

        }
    }

    float healthPerScale;
    float timeToAttack;
    bool swining;
    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("Enemy Bar");

        timeToAttack = Random.Range(timeToAttackMin, timeToAttackMin + timeToAttackRange);

        healthPerScale = healthMax / healthBar.transform.localScale.x;
        healthCurrent = healthMax;

        swining = true;
        attacking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToAttack <= 1f && swining)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 0f, 255f);
            swining = false;
        }
        if (timeToAttack <= 0.4f && attacking)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f, 255f);
            attacking = false;
        }
        if (timeToAttack <= 0)
        {
            Debug.Log("I'm attacking you!");
            Attack();
        }
        else
        {
            timeToAttack -= Time.deltaTime;
        }
        if (healthChanged)
            healthBar.transform.localScale = new Vector3(health / healthPerScale, healthBar.transform.localScale.y);

    }

    public override void Attack()
    {
        //gameManager.health -= damage;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        timeToAttack = Random.Range(timeToAttackMin, timeToAttackMin + timeToAttackRange);
        swining = true;
        attacking = true;
        if (_attackEvent != null)
        {
            _attackEvent(this);
        }
    }

    public void AddAttackEvent(attackEventDel newEvent)
    {
        _attackEvent += newEvent;
    }

    public void AddWinEvent(winEventDel newEvent)
    {
        _winEvent += newEvent;
    }
}
