using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour, IGameManager
{
    public float health
    {
        get => healthCurrent;
        set
        {
            if (defenceTimeLeft <= 0)
            {
                healthCurrent = value;
                healthBar.transform.localScale = new Vector3(healthCurrent / healthPerScale, healthBar.transform.localScale.y);
                if (healthCurrent <= 0)
                {
                    GameOver();
                }
            }
            else
            {
                staminaCurrent += 20;
            }
        }
    }
    public float healthMax;

    public float defenceTime;

    public float staminaMax;

    public float damage;

    GameObject healthBar;
    GameObject staminaBar;

    IEnemy enemy;

    float healthCurrent;
    float healthPerScale;

    float defenceTimeLeft = 0f;

    float staminaCurrent;
    float staminaPerScale;
    float staminaRecoveryTime = 0f;

    bool win
    {
        get => winCondition;
    }
    bool winCondition;

    void Start()
    {
        Time.timeScale = 1;

        healthBar = GameObject.Find("Health Bar");
        staminaBar = GameObject.Find("Stamina Bar");
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        healthCurrent = healthMax;
        healthPerScale = healthMax / healthBar.transform.localScale.x;

        staminaCurrent = staminaMax;
        staminaPerScale = staminaMax / staminaBar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (defenceTimeLeft > 0)
            defenceTimeLeft -= Time.deltaTime;
        if (staminaCurrent < staminaMax && staminaRecoveryTime <= 0)
        {
            staminaCurrent = Mathf.Clamp(staminaCurrent + 35 * Time.deltaTime, 0, staminaMax);
            staminaBar.transform.localScale = new Vector3(staminaCurrent / staminaPerScale, staminaBar.transform.localScale.y);
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
        if (enemy != null && staminaCurrent >= 10)
        {
            Debug.Log("Take this!");
            enemy.health -= damage;

            staminaCurrent = Mathf.Clamp(staminaCurrent - 10, 0, staminaMax);
            staminaBar.transform.localScale = new Vector3(staminaCurrent / staminaPerScale, staminaBar.transform.localScale.y);
            staminaRecoveryTime = 1f;
        }
        if (enemy.health <= 0)
        {
            Time.timeScale = 0f;
            winCondition = true;
        }
    }

    public void Defence()
    {
        if (staminaCurrent > 20)
        {
            defenceTimeLeft = defenceTime;
            staminaCurrent -= 20;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void TouchBegan(TouchDetail touch)
    {
        if (win)
            Restart();
        else
        {
            GameObject touchedObject = touch.getGameObject();
            if (touchedObject!=null)
                switch (touchedObject.name)
                {
                    case "Area of Attack":
                        DamageEnemy();
                        break;
                    case "Area of Defence":
                        Defence();
                        break;
                    default:
                        break;
                }
        }
            
    }
    public void TouchMoved(TouchDetail touch) { }
    public void TouchStationary(TouchDetail touch) { }
}
