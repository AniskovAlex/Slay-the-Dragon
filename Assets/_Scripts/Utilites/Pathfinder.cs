using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    class Node
    {
        public int g { get; private set; }
        public int h { get; private set; }
        public int f => g + h;
        public Vector2 position { get; private set; }

        public Node connection { get; private set; }

        public Node(int g, int h, Vector2 position)
        {
            this.g = g;
            this.h = h;
            this.position = position;
            connection = null;
        }

        public Node(Vector2 from, Vector2 to)
        {
            Vector2 dist = AbsSubstractVector2(from, to);
            int distCoast = Coast(dist);
            g = 0;
            h = distCoast;
            position = from;
            connection = null;
        }

        public void SetConnection(Node nodeBase) => connection = nodeBase;

        public void SetG(int newG) => g = newG;
        public void SetH(int newH) => h = newH;
    }

    public static List<Vector2> FindDirectPath(Tile[,] map, Vector2 from, Vector2 to)
    {
        List<Node> searchList = new List<Node>();
        List<Node> lookedList = new List<Node>();
        List<Vector2> path = new List<Vector2>();

        Node current = new Node(from, to);
        searchList.Add(current);
        while (searchList.Count != 0)
        {
            current = searchList[0];
            foreach (Node x in searchList)
            {
                if ((current.f >= x.f && current.h > x.h))
                    current = x;
            }
            if (current.position == to)
            {
                while (current.connection != null)
                {
                    path.Add(current.position);
                    current = current.connection;
                }
                break;
            }
            SearchInList(map, to, current, searchList, lookedList);
            lookedList.Add(current);
            searchList.Remove(current);
        }
        path.Reverse();
        return path;
    }

    static void SearchInList(Tile[,] map, Vector2 to, Node now, List<Node> searchList, List<Node> lookedList)
    {

        Vector2 point = now.position;
        for (int n = 0, i = 1, j = 1; n < 8; n++)
        {
            addToSearchList(map, now, to, point + new Vector2(i, j), searchList, lookedList);
            if (j == 1) j = -1;
            else
            if (j == -1) j = 0;
            if (n == 2) { i = -1; j = 1; }
            else if (n == 5) { i = 0; j = 1; }
        }
    }

    static void addToSearchList(Tile[,] map, Node from, Vector2 to, Vector2 now, List<Node> searchList, List<Node> lookedList)
    {
        if (map[(int)now.x, (int)now.y].ground == Tile.Wall)
        {
            return;
        }
        foreach (Node x in lookedList)
        {
            if (now == x.position)
            {
                return;
            }
        }
        Vector2 distH = AbsSubstractVector2(now, to);
        Vector2 distG = AbsSubstractVector2(now, from.position);
        int distHCoast = Coast(distH);
        int distGCoast = from.g + Coast(distG);
        foreach (Node x in searchList)
        {
            if (now == x.position)
            {
                if (distGCoast + distHCoast < x.f)
                {
                    x.SetG(distGCoast);
                    x.SetH(distHCoast);
                    x.SetConnection(from);
                }
                return;
            }
        }
        Node node = new Node(distGCoast, distHCoast, now);
        node.SetConnection(from);
        searchList.Add(node);

    }

    public static Vector2 AbsSubstractVector2(Vector2 a, Vector2 b)
    {
        Vector2 fin = a - b;
        fin.x = Mathf.Abs(fin.x);
        fin.y = Mathf.Abs(fin.y);
        return fin;
    }

    static int Coast(Vector2 dist)
    {

        // переименновать
        int horiz = 0;
        int vert = 0;
        horiz = (int)(Mathf.Abs(dist.x - dist.y));
        vert = (int)(Mathf.Max(dist.x, dist.y) - horiz) * 14;
        horiz *= 10;
        return horiz + vert;
    }
}
