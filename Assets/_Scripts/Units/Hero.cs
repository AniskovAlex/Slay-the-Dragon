using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : UnitDamageable
{


    public float defenceTime;

    public delegate void attackEventDel(UnitDamageable attacker);
    event attackEventDel _attackEvent;

    public delegate void loseEventDel();
    event loseEventDel _loseEvent;
    public override float health
    {
        get => healthCurrent;
        set
        {

            if (defenceTimeLeft <= 0)
            {
                healthCurrent = Mathf.Clamp(value, 0, healthMax);
                healthChanged = true;
                if (healthCurrent == 0)
                    _loseEvent();
            }
            else
            {
                staminaCurrent += 20;
            }
        }
    }


    public float stamina
    {
        get => staminaCurrent;
        set
        {
            staminaCurrent = value;
            staminaChanged = true;
            if (value < 0)
            {
                staminaRecoveryTime = 1f;

            }
        }
    }

    public float staminaMax;

    public bool staminaChanged = false;

    float defenceTimeLeft = 0f;

    float staminaCurrent;
    float staminaRecoveryTime = 0f;

    GameObject _healthBar;
    GameObject _staminaBar;

    float staminaPerScale;
    float healthPerScale;
    // Start is called before the first frame update
    void Start()
    {
        stamina = staminaMax;
        health = healthMax;
        _healthBar = GameObject.Find("Health Bar");
        _staminaBar = GameObject.Find("Stamina Bar");
        healthPerScale = healthMax / _healthBar.transform.localScale.x;
        staminaPerScale = staminaMax / _staminaBar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (defenceTimeLeft > 0)
            defenceTimeLeft -= Time.deltaTime;
        if (staminaCurrent < staminaMax && staminaRecoveryTime <= 0)
        {
            staminaCurrent = Mathf.Clamp(staminaCurrent + 35 * Time.deltaTime, 0, staminaMax);

        }
        if (staminaRecoveryTime > 0)
            staminaRecoveryTime -= Time.deltaTime;
        GraphicUpdate();
    }

    public void Defence()
    {
        if (stamina > 20)
        {
            defenceTimeLeft = defenceTime;
            staminaCurrent -= 20;
            staminaRecoveryTime = 1f;
        }
    }

    public void AddAttackEvent(attackEventDel newEvent)
    {
        _attackEvent += newEvent;
    }

    public void AddLoseEvent(loseEventDel newEvent)
    {
        _loseEvent += newEvent;
    }

    public override void Attack()
    {
        if (stamina > 10)
        {
            _attackEvent(this);
            staminaRecoveryTime = 1f;
            stamina -= 10;
        }
    }

    void GraphicUpdate()
    {
        if (healthChanged)
            _healthBar.transform.localScale = new Vector3(health / healthPerScale, _healthBar.transform.localScale.y);
        if (staminaChanged)
            _staminaBar.transform.localScale = new Vector3(stamina / staminaPerScale, _staminaBar.transform.localScale.y);
    }
}
