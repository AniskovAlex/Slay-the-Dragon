using UnityEngine;
using UnityEngine.Tilemaps;

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
    EnemyMapBehaviour _enemy;
    Tile[,] map;
    MapHero _hero;

    // Start is called before the first frame update
    void Start()
    {

        map = new Tile[mapSizeX + 1, mapSizeY + 1];

        _enemy = _enemyBody.GetComponent<EnemyMapBehaviour>();
        _hero = _heroBody.GetComponent<MapHero>();
    }

    public void createMap()
    {
        int x = innerMapSizeXMin, y = innerMapSizeYMin;
        spawnHero(x, y);
        map[x, y].ground = Tile.Road;
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

            if (x >= innerMapSizeXMax && y >= innerMapSizeYMax)
                endRoad = true;
            map[x, y].ground = Tile.Road;

            int j = Random.Range(0, 10);
            if (j == 0)
                if (!isProp(map[x, y]))
                    map[x, y].prop = Tile.Enemy;

            creatFieldAround(ref map, x, y, Random.Range(0, 3) + 3);
            int root = Random.Range(0, 10);
            if (root == 0)
                createNewRoot(ref map, x, y);
        }
        generateMap();
    }

    public void loadMap(Tile[,] map)
    {
        this.map = map;
        //Debug.Log(SaveData.GetSaveData().LoadMap()[5, 5].prop.isomPosition + "fffffff");
        generateMap();
    }

    void generateMap()
    {
        Tilemap tileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        TileBase tile = Resources.Load<TileBase>("Isometric Diamond");
        TileBase tileField = Resources.Load<TileBase>("Field");
        for (int i = 0; i <= mapSizeX; i++)
        {
            for (int j = 0; j <= mapSizeY; j++)
            {
                switch (map[i, j].ground)
                {
                    case Tile.Road:
                        tileMap.SetTile(new Vector3Int(i, j, 0), tile);
                        break;
                    case Tile.Field:
                        tileMap.SetTile(new Vector3Int(i, j, 0), tileField);
                        break;
                    default:
                        break;
                }
                switch (map[i, j].prop)
                {
                    case Tile.Hero:
                        _hero.Spawn(i, j);
                        break;
                    case Tile.Enemy:
                        Debug.Log("zzzz");
                        _enemy.Spawn(i, j);
                        break;
                    default:
                        break;
                }
            }
        }


        //Временно
        /*_enemy = Instantiate(_enemy, new Vector3(0, 2.25f, 0), Quaternion.identity);
        Debug.Log(_enemy.transform.position);
        _enemy.GetComponent<EnemyMapBehaviour>().isomPosition = RectToIsom(_enemy.transform.position);
        Debug.Log(_enemy.GetComponent<EnemyMapBehaviour>().isomPosition);
        _enemy.GetComponent<EnemyMapBehaviour>().enemy = _enemy.gameObject;
        _enemy.GetComponent<EnemyMapBehaviour>().map = this;
        Debug.Log(_enemy.GetComponent<EnemyMapBehaviour>().isomPosition);*/
    }

    void createNewRoot(ref Tile[,] map, int x, int y)
    {
        int dir = Random.Range(0, 2);
        if (dir == 0)
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
                    if (dir == 0)
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
            int j = Random.Range(0, 10);
            if (j == 0)
                if (!isProp(map[x, y]))
                    map[x, y].prop = Tile.Enemy;
            creatFieldAround(ref map, x, y, Random.Range(0, 5) + 3);
        }
    }

    void creatFieldAround(ref Tile[,] map, int x, int y, int tilesLeft)
    {
        if (tilesLeft > 0)
        {
            if (map[x, y].ground == Tile.Wall)
            {
                map[x, y].ground = Tile.Field;
            }
            int x0 = x, y0 = y;
            x0++;
            if (x0 < mapSizeX)
                if (IsWall(map[x0, y0]))
                {
                    creatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            x0 = x;
            x0--;
            if (x0 > 0)
                if (IsWall(map[x0, y0]))
                {
                    creatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            x0 = x;
            y0++;
            if (y0 < mapSizeY)
                if (IsWall(map[x0, y0]))
                {
                    creatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            y0 = y;
            y0--;
            if (y0 > 0)
                if (IsWall(map[x0, y0]))
                {
                    creatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
        }
    }

    bool spawnHero(int x, int y)
    {
        if (!isValid(x, y)) return false;
        if (map[x, y].prop == null) map[x, y].prop = Tile.Hero;
        _hero.isomPosition = new Vector2(x, y);
        return true;
    }

    bool IsWall(Tile tile)
    {
        if (tile.ground == Tile.Wall)
            return true;
        else
            return false;
    }

    bool isProp(Tile tile)
    {
        if (tile.prop != null)
            return true;
        else
            return false;
    }

    public bool movePlayer(Vector2 mapDirection, Vector2 playerDirection)
    {
        Vector2 newPlayerPosition = _hero.isomPosition;
        newPlayerPosition += mapDirection;
        Debug.LogFormat("x: {0} y: {1}", (int)newPlayerPosition.x, (int)newPlayerPosition.y);
        if (!IsWall(map[(int)newPlayerPosition.x, (int)newPlayerPosition.y]))
        {
            if (isProp(map[(int)newPlayerPosition.x, (int)newPlayerPosition.y]))
            {
                _enemy.OnTouch();
                map[(int)_hero.isomPosition.x, (int)_hero.isomPosition.y].prop = null;
                _hero.isomPosition = newPlayerPosition;
                map[(int)_hero.isomPosition.x, (int)_hero.isomPosition.y].prop = Tile.Hero;
            }
            else
            {
                map[(int)_hero.isomPosition.x, (int)_hero.isomPosition.y].prop = null;
                _hero.isomPosition = newPlayerPosition;
                map[(int)_hero.isomPosition.x, (int)_hero.isomPosition.y].prop = Tile.Hero;
                _hero.transform.position += (Vector3)(playerDirection);
                return true;
            }
        }
        return false;
    }

    public bool moveEnemy(EnemyMapBehaviour enemy, Vector2 position)
    {
        Vector2 buf = enemy.isomPosition;
        bool moved = !IsWall(map[(int)buf.x, (int)buf.y])||!isProp(map[(int)buf.x, (int)buf.y]);
        if (moved)
        {
            map[(int)position.x, (int)position.y].prop = Tile.Enemy;
            map[(int)buf.x, (int)buf.y].prop = null;
        }
        return moved;
    }

    public static Vector2 RectToIsom(Vector2 direction)
    {
        int x = (int)(direction.x + 2 * direction.y - 0.5);
        int y = (int)(-direction.x + 2 * direction.y - 0.5);
        return new Vector2(x, y);
    }

    //Сделать
    public static Vector2 IsomToRect(Vector2 direction)
    {
        double x = 0.5f * direction.x - 0.5f * direction.y;
        double y = 0.25f * direction.x + 0.25f * direction.y + 0.25;
        return new Vector2((float)x, (float)y);
    }

    public Tile[,] GetTileMap()
    {
        return map;
    }

    bool isValid(int x, int y)
    {
        if (((x >= 0 && x <= mapSizeX) && ((y >= 0 && y <= mapSizeY))))
            return true;
        else return false;
    }
}
