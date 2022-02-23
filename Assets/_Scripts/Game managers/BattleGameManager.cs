using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleGameManager : MonoBehaviour, IGameManager
{
    delegate void _attackEvent();

    TextMesh text;

    Hero _hero;
    Enemy _enemy;

    bool win
    {
        get => winCondition;
    }
    bool winCondition;

    void Start()
    {
        Time.timeScale = 1;

        _hero = GameObject.Find("Hero").GetComponent<Hero>();
        Hero.attackEventDel heroAttackEvent = AttackTriggered;
        _hero.AddAttackEvent(heroAttackEvent);
        Hero.loseEventDel loseEvent = losing;
        _hero.AddLoseEvent(loseEvent);


        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        Enemy.attackEventDel enemyAttackEvent = AttackTriggered;
        _enemy.AddAttackEvent(enemyAttackEvent);
        Enemy.winEventDel winEvent = winning;
        _enemy.AddWinEvent(winEvent);

        text = GameObject.Find("Text").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.anyKeyDown)
        {
            Debug.Log("aa");
            StartCoroutine(loadScene());
        }*/
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
            
        }

    }
    public void TouchMoved(TouchDetail touch) { }
    public void TouchStationary(TouchDetail touch) { }

    public void AttackTriggered(UnitDamageable attacker)
    {
        if (attacker.tag == "Enemy")
        {
            Attack(attacker, _hero);
        }
        else
        {
            Attack(attacker, _enemy);
        }
    }

    public void Attack(UnitDamageable attacker, UnitDamageable defencer)
    {
        defencer.health -= attacker.damage;
    }

    private void winning()
    {
        winCondition = true;
        Time.timeScale = 0;
        text.text = "You win!";
        StartCoroutine(loadScene());
    }

    private void losing()
    {
        winCondition = true;
        Time.timeScale = 0;
        text.text = "You lose...";
    }

    public IEnumerator loadScene()
    {
        /*GameObject saveObject = GameObject.FindGameObjectWithTag("Save");
        DontDestroyOnLoad(saveObject);*/
        Time.timeScale = 0;
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            Debug.Log("DD");
            yield return null;
        }
        Debug.Log("D");
        //SceneManager.MoveGameObjectToScene(saveObject, SceneManager.GetSceneAt(0));
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
