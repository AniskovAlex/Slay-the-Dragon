using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class TilemapBehaviour : MonoBehaviour
{
    public int mapSizeX = 17;
    public int mapSizeY = 17;
    public int innerMapSizeXMin = 3;
    public int innerMapSizeYMin = 3;
    public int innerMapSizeXMax = 13;
    public int innerMapSizeYMax = 13;
    public int chanceLeftMin = 1, chanceLeftMax = 4;
    public int chanceRightMin = 5, chanceRightMax = 8;
    public int chanceDownMin = 9, chanceDownMax = 9;
    public GameObject _enemyBody;
    public GameObject _heroBody;
    public GameObject _finish;
    public GameObject _healBody;
    EnemyMapBehaviour _enemy;
    Heal _heal;
    Tile[,] _map;
    MapHero _hero;
    MapHero _heroCurrent;
    public List<UnitProp> _propList;

    public bool playerMoved = false;

    // Start is called before the first frame update
    void Start()
    {

        _map = new Tile[mapSizeX + 1, mapSizeY + 1];

        _enemy = _enemyBody.GetComponent<EnemyMapBehaviour>();
        _hero = _heroBody.GetComponent<MapHero>();
        _heal = _healBody.GetComponent<Heal>();
    }

    public void CreateMap()
    {
        int x = innerMapSizeXMin, y = innerMapSizeYMin;
        _map[x, y].prop = _hero;
        _map[x, y].ground = Tile.Road;
        bool endRoad = false;

        while (!endRoad)
        {
            int i = Random.Range(0, 2);
            if (i == 0)
                y++;
            else
                x++;
            {
                if (y > innerMapSizeYMax)
                {
                    y--;
                    x++;
                }
                if (x > innerMapSizeXMax)
                {
                    y++;
                    x--;
                }
            }
            _map[x, y].ground = Tile.Road;
            if (x >= innerMapSizeXMax && y >= innerMapSizeYMax)
            {
                endRoad = true;
                _map[x, y].ground = Tile.Finish;
                _map[x, y].prop = _finish.GetComponent<Finish>();
            }
            

            int j = Random.Range(0, 25);
            if (j == 0)
                if (!IsProp(_map[x, y]))
                    _map[x, y].prop = _enemy;

            CreatFieldAround(ref _map, x, y, Random.Range(0, 3) + 3);
            int root = Random.Range(0, 10);
            if (root == 0)
                CreateNewRoot(ref _map, x, y);
        }
        _map[8, 5].prop = _enemy;
        GenerateMap();
    }

    public void LoadMap(Tile[,] map)
    {
        _map = map;
        GenerateMap();
    }

    void GenerateMap()
    {
        Tilemap tileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        TileBase tile = Resources.Load<TileBase>("Isometric Diamond");
        TileBase tileField = Resources.Load<TileBase>("Field");
        TileBase tileFinish = Resources.Load<TileBase>("Finish");
        for (int i = 0; i <= mapSizeX; i++)
        {
            for (int j = 0; j <= mapSizeY; j++)
            {
                switch (_map[i, j].ground)
                {
                    case Tile.Road:
                        tileMap.SetTile(new Vector3Int(i, j, 0), tile);
                        break;
                    case Tile.Field:
                        tileMap.SetTile(new Vector3Int(i, j, 0), tileField);
                        break;
                    case Tile.Finish:
                        tileMap.SetTile(new Vector3Int(i, j, 0), tileFinish);
                        break;
                    default:
                        break;
                }
                if (_map[i, j].prop != null)
                {
                    _propList.Add(_map[i, j].prop.Spawn(i, j));
                }
            }
        }
        foreach(UnitProp x in _propList)
        {
            Debug.Log(x.name + " "+ x.isomPosition);
        }
    }

    void CreateNewRoot(ref Tile[,] map, int x, int y)
    {
        int direction = Random.Range(0, 2);
        if (direction == 0)
        {
            chanceLeftMax -= 2;
            chanceRightMin -= 2;
        }
        else
        {
            chanceLeftMax += 2;
            chanceRightMin += 2;
        }
        int steps = Random.Range(0, 10) + 6;
        for (int i = 0, step = Random.Range(1, 10); i < steps; i++, step = Random.Range(1, 10))
        {
            switch (step)
            {
                case int n when (n <= chanceLeftMax && n >= chanceLeftMin):
                    x++;
                    break;
                case int n when (n <= chanceRightMax && n >= chanceRightMin):
                    y++;
                    break;
                case int n when (n <= chanceDownMax && n >= chanceDownMin):
                    if (direction == 0)
                        x--;
                    else
                        y--;
                    break;
                default:
                    break;
            }
            if (x > innerMapSizeXMax || y > innerMapSizeYMax || x < innerMapSizeXMin || y < innerMapSizeYMin)
                break;
            map[x, y].ground = Tile.Road;
            // отедльный метод!!!!!!!!!!!!
            int j = Random.Range(0, 25);
            if (j == 0)
                if (!IsProp(map[x, y]))
                    map[x, y].prop = _enemy;
            CreatFieldAround(ref map, x, y, Random.Range(0, 5) + 3);
        }
    }

    void CreatFieldAround(ref Tile[,] map, int x, int y, int tilesLeft)
    {
        if (tilesLeft > 0)
        {
            if (map[x, y].ground == Tile.Wall)
            {
                map[x, y].ground = Tile.Field;
                int j = Random.Range(0, 100);
                if (j == 0)
                    if (!IsProp(_map[x, y]))
                        _map[x, y].prop = _heal;
            }
            int x0 = x, y0 = y;
            x0++;
            if (x0 < mapSizeX)
                if (IsWall(map[x0, y0]))
                {
                    CreatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            x0 = x;
            x0--;
            if (x0 > 0)
                if (IsWall(map[x0, y0]))
                {
                    CreatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            x0 = x;
            y0++;
            if (y0 < mapSizeY)
                if (IsWall(map[x0, y0]))
                {
                    CreatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            y0 = y;
            y0--;
            if (y0 > 0)
                if (IsWall(map[x0, y0]))
                {
                    CreatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
        }
    }

    bool IsWall(Tile tile)
    {
        if (tile.ground == Tile.Wall)
            return true;
        else
            return false;
    }

    bool IsProp(Tile tile)
    {
        if (tile.prop != null)
            return true;
        else
            return false;
    }

    public bool MovePlayer(Vector2 mapDirection, Vector2 playerDirection, MapHero hero)
    {
        Vector2 newPlayerPosition = hero.isomPosition;
        newPlayerPosition += mapDirection;
       // Debug.LogFormat("x: {0} y: {1}", (int)newPlayerPosition.x, (int)newPlayerPosition.y);
        if (!IsWall(_map[(int)newPlayerPosition.x, (int)newPlayerPosition.y]))
        {
            if (IsProp(_map[(int)newPlayerPosition.x, (int)newPlayerPosition.y]))
            {
                //_map[(int)hero.isomPosition.x, (int)hero.isomPosition.y].prop = null;
                //hero.isomPosition = newPlayerPosition;
                //_map[(int)hero.isomPosition.x, (int)hero.isomPosition.y].prop = _hero;
                Debug.Log("aaaaaaaaaaa");
                
                _map[(int)newPlayerPosition.x, (int)newPlayerPosition.y].prop.OnTouch(newPlayerPosition);
            }
            else
            {

                _map[(int)hero.isomPosition.x, (int)hero.isomPosition.y].prop = null;
                hero.isomPosition = newPlayerPosition;
                _map[(int)hero.isomPosition.x, (int)hero.isomPosition.y].prop = _hero;
                hero.transform.position += (Vector3)(playerDirection);
                playerMoved = true;
                return true;
            }
        }
        return false;
    }

    public bool MoveEnemy(EnemyMapBehaviour enemy, Vector2 newPosition)
    {
        Vector2 currentPosition = enemy.isomPosition;
        bool touched = IsProp(_map[(int)newPosition.x, (int)newPosition.y]);
        bool moved = !IsWall(_map[(int)newPosition.x, (int)newPosition.y]) &&!touched;

        if (touched)
        {
            if(_map[(int)newPosition.x, (int)newPosition.y].prop == _hero)
            {
                _map[(int)currentPosition.x, (int)currentPosition.y].prop = null;
                enemy.OnTouch(newPosition);
            }
        }

        if (moved)
        {
            //Debug.Log(newPosition);
            //Debug.Log(_map[(int)newPosition.x, (int)newPosition.y].ground);
            _map[(int)newPosition.x, (int)newPosition.y].prop = _enemy;
            _map[(int)currentPosition.x, (int)currentPosition.y].prop = null;
        }
        //Debug.Log(touched);
        //Debug.Log(moved);

        return moved;
    }

    public static Vector2 RectToIsom(Vector2 direction)
    {
        int x = (int)(direction.x + 2 * direction.y - 0.5);
        int y = (int)(-direction.x + 2 * direction.y - 0.5);
        return new Vector2(x, y);
    }

    public static Vector2 IsomToRect(Vector2 direction)
    {
        double x = 0.5f * direction.x - 0.5f * direction.y;
        double y = 0.25f * direction.x + 0.25f * direction.y + 0.25;
        return new Vector2((float)x, (float)y);
    }

    public Tile[,] GetTileMap()
    {
        return _map;
    }

    public Vector2 GetPlayerPosition()
    {
        return _heroCurrent.isomPosition;
    }

    public void SetHero(MapHero hero)
    {
        if (_heroCurrent == null) _heroCurrent = hero;
    }

    bool IsValid(int x, int y)
    {
        if (((x >= 0 && x <= mapSizeX) && ((y >= 0 && y <= mapSizeY))))
            return true;
        else return false;
    }

    public void SwapProp(UnitProp to, Vector2 position)
    {
        _map[(int)position.x, (int)position.y].prop = to;
        Debug.Log(position);
        foreach (UnitProp x in _propList)
        {
            if (x.isomPosition == position)
            {
                Debug.Log(x.isomPosition + "dddddddd");
                _propList.Remove(x);
                Destroy(x.gameObject);
                _propList.Add(to.Spawn((int)position.x, (int)position.y));
                foreach (UnitProp y in _propList)
                {
                    Debug.Log(y.name + " " + y.isomPosition);
                }
                return;
            }
        }
    }
}
