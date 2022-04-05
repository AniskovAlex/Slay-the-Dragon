using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleGameManager : MonoBehaviour
{
    delegate void _attackEvent();

    public GameObject _screen;
    public Text _textOnScreen;

    Hero _hero;
    Enemy _enemy;

    bool winCondition = false;
    bool loseCondition = false;

    void Start()
    {
        Time.timeScale = 1;
        _hero = GameObject.Find("Hero").GetComponent<Hero>();
        if (SaveData.GetSaveData().LoadHealth() != 0)
            _hero.health = SaveData.GetSaveData().LoadHealth();
        else
            _hero.health = _hero.healthMax;
        Hero.loseEventDel loseEvent = Losing;
        Hero.attackEventDel heroAttackEvent = AttackTriggered;
        _hero.AddAttackEvent(heroAttackEvent);
        _hero.AddLoseEvent(loseEvent);

        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        Enemy.attackEventDel enemyAttackEvent = AttackTriggered;
        Enemy.winEventDel winEvent = Winning;
        _enemy.AddAttackEvent(enemyAttackEvent);
        _enemy.AddWinEvent(winEvent);

    }

    // Update is called once per frame
    void Update()
    {     
        /*_screen.enabled = false;
        Debug.Log("Sdsfdsfdsf");
        Debug.Log(_screen.enabled);*/
        /*if (Input.anyKeyDown)
        {
            Debug.Log("aa");
            PreloadSceen();
        }*/
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

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

    private void Winning()
    {
        winCondition = true;
        Time.timeScale = 0;
        //_text.text = "You win!";
        _screen.SetActive(true);
        _textOnScreen.text = "Ты победил!";
    }
    private void Losing()
    {
        loseCondition = true;
        Time.timeScale = 0;
        //_text.text = "You lose...";
        _screen.SetActive(true);
        _textOnScreen.text = "Ты проиграл!!";
    }

    public void PreloadSceen()
    {
        
        if (loseCondition)
        {
            SaveData.GetSaveData().ResetData();
        }
        if (winCondition)
        {
            SaveData.GetSaveData().SaveHealth((int)_hero.health);
        }
        Time.timeScale = 0;
        SaveData.GetSaveData();
        StartCoroutine(loadScene());
    }

    public IEnumerator loadScene()
    {
        /*GameObject saveObject = GameObject.FindGameObjectWithTag("Save");
        DontDestroyOnLoad(saveObject);*/
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            //Debug.Log("DD");
            yield return null;
        }
        //Debug.Log("D");
        //SceneManager.MoveGameObjectToScene(saveObject, SceneManager.GetSceneAt(0));
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
