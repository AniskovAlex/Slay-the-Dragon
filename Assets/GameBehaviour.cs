using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    private GameObject healthBar;
    private GameObject staminaBar;
    private IEnemy enemy;
    public float HP
    {
        get => currentHp;
        set
        {
            if (defenceTimeLeft <= 0)
            {
                currentHp = value;
                healthBar.transform.localScale = new Vector3(currentHp / HPPerScale, healthBar.transform.localScale.y);
                if (currentHp <= 0)
                {
                    GameOver();
                }
            }
        }
    }
    private float maxHP = 100f;
    private float currentHp = 100f;
    private float HPPerScale;
    private float defenceTimeLeft = 0f;
    private float defenceTime = 0.4f;

    private float maxStamina = 100f;
    private float currentStamina;
    private float staminaPerScale;
    private float staminaRecoveryTime = 0f;

    private float damage = 2.5f;

    public bool win
    {
        get => winCondition;
    }
    private bool winCondition;

    void Start()
    {
        Time.timeScale = 1;
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        healthBar = GameObject.Find("HP Bar");
        HPPerScale = maxHP / healthBar.transform.localScale.x;
        currentStamina = maxStamina;
        staminaBar = GameObject.Find("Stamina Bar");
        staminaPerScale = maxStamina / staminaBar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (defenceTimeLeft > 0)
            defenceTimeLeft -= Time.deltaTime;
        if (currentStamina < maxStamina && staminaRecoveryTime<=0)
        {
            currentStamina = Mathf.Clamp(currentStamina + 35 * Time.deltaTime, 0, maxStamina);
            staminaBar.transform.localScale = new Vector3(currentStamina / staminaPerScale, staminaBar.transform.localScale.y);
        }
        if (staminaRecoveryTime > 0)
            staminaRecoveryTime -= Time.deltaTime;
    }
    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void DamageEnemy()
    {
        if (enemy != null && currentStamina >= 10)
        {
            Debug.Log("Take this!");
            enemy.HP -= damage;
            currentStamina = Mathf.Clamp(currentStamina - 10, 0, maxStamina);
            staminaBar.transform.localScale = new Vector3(currentStamina / staminaPerScale, staminaBar.transform.localScale.y);
            staminaRecoveryTime = 1f;
        }
        if (enemy.HP <= 0)
        {
            Time.timeScale = 0f;
            winCondition = true;
        }
    }

    public void Defence()
    {
        defenceTimeLeft = defenceTime;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
