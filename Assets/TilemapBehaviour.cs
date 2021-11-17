using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TilemapBehaviour : MonoBehaviour
{
    public int mapSizeX = 17;
    public int mapSizeY = 17;
    public int innerMadSizeXMin = 3;
    public int innerMapSizeYMin = 3;
    public int innerMapSizeXMax = 13;
    public int innerMapSizeYMax = 13;
    public int chanceLeftMin = 1, chanceLeftMax = 4;
    public int chanceRightMin = 5, chanceRightMax = 8;
    public int chanceDownMin = 9, chanceDownMax = 9;
    Tile[,] map;
    GameObject player;
    Vector2 playerPosition;

    // Start is called before the first frame update
    void Start()
    {

        map = new Tile[mapSizeX + 1, mapSizeY + 1];
        player = GameObject.Find("Square");
        int x = innerMadSizeXMin, y = innerMapSizeYMin;
        bool endRoad = false;

        map[x, y].ground = Tile.Road;
        map[x, y].prop = Tile.Player;
        playerPosition = new Vector2(x, y);

        // player.transform.position += (Vector3)(playerPosition * new Vector2(0.25f, 0.5f));

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

            creatFieldAround(ref map, x, y, Random.Range(0, 3) + 3);
            int root = Random.Range(0, 10);
            if (root == 0)
                createNewRoot(ref map, x, y);
        }

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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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
            if (x > innerMapSizeXMax || y > innerMapSizeYMax || x < innerMadSizeXMin || y < innerMapSizeYMin)
                break;
            map[x, y].ground = Tile.Road;
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
    bool IsWall(Tile tile)
    {
        if (tile.ground == Tile.Wall)
            return true;
        else
            return false;
    }

    public bool movePlayer(Vector2 mapDirection, Vector2 playerDirection)
    {
        Vector2 newPlayerPosition = playerPosition;
        newPlayerPosition += mapDirection;
        Debug.LogFormat("x: {0} y: {1}", (int)newPlayerPosition.x, (int)newPlayerPosition.y);
        if (!IsWall(map[(int)newPlayerPosition.x, (int)newPlayerPosition.y]))
        {
            map[(int)playerPosition.x, (int)playerPosition.y].prop = 100;
            playerPosition = newPlayerPosition;
            map[(int)playerPosition.x, (int)playerPosition.y].prop = Tile.Player;
            player.transform.position += (Vector3)(playerDirection);
            return true; 
        }
        return false;
    }

    /*private Vector2 RectToIsom(Vector2 direction)
    {
        return new Vector2(Mathf.Sign(Mathf.Sign(direction.y) + Mathf.Sign(direction.x)), Mathf.Sign(Mathf.Sign(direction.y) + (-1) * Mathf.Sign(direction.x)));
    }*/
}
