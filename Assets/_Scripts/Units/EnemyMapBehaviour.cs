using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMapBehaviour : UnitProp
{
    public TilemapBehaviour map;
    public GameObject enemy;
    private int vision = 5;
    float timeDelayLeft = 0f;
    float timeDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        if (isomPosition == null)
        {
            isomPosition = new Vector2(0, 0);
        }
        map = GameObject.Find("Map Generater").GetComponent<TilemapBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        walk();
        if (timeDelayLeft > 0) timeDelayLeft -= Time.deltaTime;
    }

    void seekPlayerPosition()
    {

    }

    void walk()
    {
        
        if (timeDelayLeft <= 0)
        {
            Debug.Log("EEEEEEEEEEE");
            switch (Random.Range(0, 8))
            {
                //косяк где-то здесь наверное
                case 0:
                    if (map.moveEnemy(this, isomPosition + new Vector2(1, 1)))
                    {
                        isomPosition += new Vector2(1, 1);
                        enemy.transform.position += (Vector3)Vector2.up * 0.5f;
                    }
                    break;
                case 1:
                    if (map.moveEnemy(this, isomPosition + new Vector2(1, 0)))
                    {
                        isomPosition += new Vector2(1, 0);
                        enemy.transform.position += (Vector3)(Vector2.up * 0.25f + Vector2.right * 0.5f);
                    }
                    break;
                case 2:
                    if (map.moveEnemy(this, isomPosition + new Vector2(1, -1)))
                    {
                        isomPosition += new Vector2(1, -1);
                        enemy.transform.position += (Vector3)Vector2.right;
                    }
                    break;
                case 3:
                    if (map.moveEnemy(this, isomPosition + new Vector2(0, -1)))
                    {
                        isomPosition += new Vector2(0, -1);
                        enemy.transform.position += (Vector3)(Vector2.down * 0.25f + Vector2.right * 0.5f);
                    }
                    break;
                case 4:
                    if (map.moveEnemy(this, isomPosition + new Vector2(-1, -1)))
                    {
                        isomPosition += new Vector2(-1, -1);
                        enemy.transform.position += (Vector3)(Vector2.down * 0.5f);
                    }
                    break;
                case 5:
                    if (map.moveEnemy(this, isomPosition + new Vector2(-1, 0)))
                    {
                        isomPosition += new Vector2(-1, 0);
                        enemy.transform.position += (Vector3)(Vector2.down * 0.25f + Vector2.left * 0.5f);
                    }
                    break;
                case 6:
                    if (map.moveEnemy(this, isomPosition + new Vector2(-1, 1)))
                    {
                        isomPosition += new Vector2(-1, 1);
                        enemy.transform.position += (Vector3)Vector2.left;
                    }
                    break;
                case 7:
                    if (map.moveEnemy(this, isomPosition + new Vector2(0, 1)))
                    {
                        isomPosition += new Vector2(0, 1);
                        enemy.transform.position += (Vector3)(Vector2.up * 0.25f + Vector2.left * 0.5f);
                    }
                    break;
                default:
                    break;
            }
            timeDelayLeft = timeDelay;
        }
    }

    public override void Spawn(int x, int y)
    {
        GameObject _enemy = Instantiate(enemy, TilemapBehaviour.IsomToRect(new Vector2(x, y)), Quaternion.identity);
        _enemy.GetComponent<EnemyMapBehaviour>().isomPosition = new Vector2(x, y);
        
        Debug.Log(enemy);
    }

    public override void OnTouch()
    {
        GameObject.Find("Game Manager").GetComponent<MapGameManager>().loadScene();
    }
}
