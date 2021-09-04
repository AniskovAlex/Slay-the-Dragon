using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public float health
    {
        get => healthCurrent;
        set
        {
            healthCurrent = value;
            healthBar.transform.localScale = new Vector3(healthCurrent / healthPerScale, healthBar.transform.localScale.y);
            if (healthCurrent <= 0)
            {
                Debug.Log("I'm dead!");
            }
        }
    }
    public float healthMax;

    public float timeToAttackMin;
    public float timeToAttackRange;
    public float damage;

    GameBehaviour gameManager;
    GameObject healthBar;

    float healthCurrent;
    float healthPerScale;
    float timeToAttack;
    bool swining;
    bool attacking;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
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
    }

    private void Attack()
    {
        gameManager.health -= damage;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        timeToAttack = Random.Range(timeToAttackMin, timeToAttackMin + timeToAttackRange);

        swining = true;
        attacking = true;
    }

}
