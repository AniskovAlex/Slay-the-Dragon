using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    GameObject healthBar;
    float HPPerScale;
    float minTimeToAttack = 2f;
    float rangeTimeToAttack = 5f;
    float clockToAttack;
    float damage;
    bool f;
    GameBehaviour gameManager;
    public float HP
    {
        get => currentHp;
        set
        {
            currentHp = value;
            healthBar.transform.localScale = new Vector3(currentHp / HPPerScale, healthBar.transform.localScale.y);
            if (currentHp <= 0)
            {
                Debug.Log("I'm dead!");
            }
        }
    }
    float maxHP = 100f;
    float currentHp;
    // Start is called before the first frame update
    void Start()
    {
        clockToAttack = Random.Range(minTimeToAttack, minTimeToAttack + rangeTimeToAttack);
        Debug.Log(clockToAttack);
        damage = 20;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
        currentHp = maxHP;
        healthBar = GameObject.Find("Enemy Bar");
        HPPerScale = maxHP / healthBar.transform.localScale.x;
        f = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (clockToAttack <= 1 && f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f, 255f);
            Debug.Log(gameObject.GetComponent<SpriteRenderer>().color);
            f = false;
        }
        if (clockToAttack <= 0)
        {
            Attack();
            Debug.Log("I'm attacking you!");
            clockToAttack = Random.Range(minTimeToAttack, minTimeToAttack + rangeTimeToAttack);
            Debug.Log(clockToAttack);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
            f = true;
        }
        else
        {
            clockToAttack -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        gameManager.HP -= damage;
    }

}
