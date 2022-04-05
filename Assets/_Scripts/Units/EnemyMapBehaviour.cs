using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMapBehaviour : UnitProp
{
    TilemapBehaviour _map;
    public GameObject _enemy;
    public int vision = 3;
    float timeToMoveLeft = 0f;
    public float timeToMoveMin = 1f;
    public float timeToMoveRange = 1.4f;
    Vector2 distToHeroVect = new Vector2(0, 0);
    bool chasing = false;
    List<Vector2> pathToHero;

    // Start is called before the first frame update
    void Start()
    {
        if (isomPosition == null)
        {
            isomPosition = new Vector2(0, 0);
        }
        timeToMoveLeft = timeToMoveMin;
        _map = GameObject.Find("Map Generater").GetComponent<TilemapBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToMoveLeft <= 0)
        {
            seekPlayerPosition();
            if (!chasing)
                Walk();
            else
                Chase();
        }
        if (timeToMoveLeft > 0) timeToMoveLeft -= Time.deltaTime;
    }

    void seekPlayerPosition()
    {
        /*if (chasing)
            return;*/
        distToHeroVect = (isomPosition - _map.GetPlayerPosition());
        distToHeroVect.x = Mathf.Abs(distToHeroVect.x);
        distToHeroVect.y = Mathf.Abs(distToHeroVect.y);
        if (vision >= distToHeroVect.x || vision >= distToHeroVect.y)
        {
            chasing = true;
            pathToHero = Pathfinder.FindDirectPath(_map.GetTileMap(), isomPosition, _map.GetPlayerPosition());
        }
        else chasing = false;
    }

    void Walk()
    {
        switch (Random.Range(0, 8))
        {
            //косяк где-то здесь наверное
            case 0:
                if (_map.MoveEnemy(this, isomPosition + new Vector2(1, 1)))
                {
                    isomPosition += new Vector2(1, 1);
                    _enemy.transform.position += (Vector3)Vector2.up * 0.5f;
                }
                break;
            case 1:
                if (_map.MoveEnemy(this, isomPosition + new Vector2(1, 0)))
                {
                    isomPosition += new Vector2(1, 0);
                    _enemy.transform.position += (Vector3)(Vector2.up * 0.25f + Vector2.right * 0.5f);
                }
                break;
            case 2:
                if (_map.MoveEnemy(this, isomPosition + new Vector2(1, -1)))
                {
                    isomPosition += new Vector2(1, -1);
                    _enemy.transform.position += (Vector3)Vector2.right;
                }
                break;
            case 3:
                if (_map.MoveEnemy(this, isomPosition + new Vector2(0, -1)))
                {
                    isomPosition += new Vector2(0, -1);
                    _enemy.transform.position += (Vector3)(Vector2.down * 0.25f + Vector2.right * 0.5f);
                }
                break;
            case 4:
                if (_map.MoveEnemy(this, isomPosition + new Vector2(-1, -1)))
                {
                    isomPosition += new Vector2(-1, -1);
                    _enemy.transform.position += (Vector3)(Vector2.down * 0.5f);
                }
                break;
            case 5:
                if (_map.MoveEnemy(this, isomPosition + new Vector2(-1, 0)))
                {
                    isomPosition += new Vector2(-1, 0);
                    _enemy.transform.position += (Vector3)(Vector2.down * 0.25f + Vector2.left * 0.5f);
                }
                break;
            case 6:
                if (_map.MoveEnemy(this, isomPosition + new Vector2(-1, 1)))
                {
                    isomPosition += new Vector2(-1, 1);
                    _enemy.transform.position += (Vector3)Vector2.left;
                }
                break;
            case 7:
                if (_map.MoveEnemy(this, isomPosition + new Vector2(0, 1)))
                {
                    isomPosition += new Vector2(0, 1);
                    _enemy.transform.position += (Vector3)(Vector2.up * 0.25f + Vector2.left * 0.5f);
                }
                break;
            default:
                break;
        }
        timeToMoveLeft = Random.Range(timeToMoveMin, timeToMoveMin + timeToMoveRange);
        //Debug.Log(isomPosition);
    }

    void Chase()
    {
        if (pathToHero.Count > 0)
        {
                if (_map.MoveEnemy(this, pathToHero[0]))
                {
                    isomPosition = pathToHero[0];
                    _enemy.transform.position = TilemapBehaviour.IsomToRect(pathToHero[0]);
                    pathToHero.RemoveAt(0);
                }
                timeToMoveLeft = timeToMoveMin;
        }
        else
            chasing = false;
    }

    public override UnitProp Spawn(int x, int y)
    {
        GameObject enemy = Instantiate(_enemy, TilemapBehaviour.IsomToRect(new Vector2(x, y)), Quaternion.identity);
        enemy.GetComponent<EnemyMapBehaviour>().isomPosition = new Vector2(x, y);
        return this;

        //Debug.Log(enemy);
    }

    public override void OnTouch()
    {
        GameObject.Find("Game Manager").GetComponent<MapGameManager>().loadScene();
    }
}
