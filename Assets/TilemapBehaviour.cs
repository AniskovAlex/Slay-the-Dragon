using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TilemapBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        int[,] map = new int[18, 18];
        int x = 0, y = 0;
        bool flag = false;
        map[x, y] = 1;
        while (!flag)
        {
            int i = Random.Range(0, 2);
            Debug.Log(i);
            if (i == 0)
                y++;
            else
                x++;
            {
                if (y > 17)
                {
                    y--;
                    x++;
                }
                if (x > 17)
                {
                    y++;
                    x--;
                }
            }
            if (x >= 17 && y >= 17)
                flag = true;
            Debug.Log(x + " " + y);
            map[x, y] = 1;
            creatFieldAround(ref map, x, y, Random.Range(0, 3) + 3);
            int root = Random.Range(0, 10);
            if (root == 0)
                createNewRoot(ref map, x, y);
        }
        GameObject test = GameObject.Find("Tilemap");
        Tilemap tileMap = test.GetComponent<Tilemap>();
        TileBase tile = Resources.Load<TileBase>("Isometric Diamond");
        TileBase tileField = Resources.Load<TileBase>("Field");
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                switch (map[i, j])
                {
                    case 1:
                        tileMap.SetTile(new Vector3Int(i, j, 0), tile);
                        break;
                    case 2:
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

    void createNewRoot(ref int[,] map, int x, int y)
    {
        int chanceLeftMin = 1, chanceLeftMax = 4;
        int chanceRightMin = 5, chanceRightMax = 8;
        int chanceDownMin = 9, chanceDownMax = 9;
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
            if (x > 17 || y > 17 || x < 0 || y < 0)
                break;
            map[x, y] = 1;
            creatFieldAround(ref map, x, y, Random.Range(0, 5) + 3);
        }
    }

    void creatFieldAround(ref int[,] map, int x, int y, int tilesLeft)
    {
        if (tilesLeft > 0)
        {
            if (map[x, y] == 0)
            {
                map[x, y] = 2;
            }
            int x0 = x, y0 = y;
            x0++;
            if (x0 < 17)
                if (map[x0, y0] == 0)
                {
                    creatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            x0 = x;
            x0--;
            if (x0 > 0)
                if (map[x0, y0] == 0)
                {
                    creatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            x0 = x;
            y0++;
            if (y0 < 17)
                if (map[x0, y0] == 0)
                {
                    creatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
            y0 = y;
            y0--;
            if (y0 > 0)
                if (map[x0, y0] == 0)
                {
                    creatFieldAround(ref map, x0, y0, tilesLeft - 1);
                }
        }
    }
}