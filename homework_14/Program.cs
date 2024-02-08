using System;
using System.Collections.Generic;

public class Program
{
    //MAZE https://acmp.ru/asp/do/index.asp?main=task
    public struct Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public static bool HasWall(int[,] walls, int x, int y)
    {
        if (x == -1)
        {
            return y >= 1;
        }
        if (y == -1)
        {
            return x >= 1;
        }
        if (y == walls.GetLength(0))
        {
            return x < walls.GetLength(0) - 1;
        }
        if (x == walls.GetLength(0))
        {
            return y < walls.GetLength(0) - 1;
        }

        return walls[x, y] == 1;
    }
    public static int Rec(int[,] walls, int x, int y, HashSet<Point> used)
    {
        if (x >= walls.GetLength(0) || y >= walls.GetLength(0))
        {
            return 0;
        }

        var wallsC = (HasWall(walls, x - 1, y) ? 1 : 0) +
            (HasWall(walls, x + 1, y) ? 1 : 0) +
            (HasWall(walls, x, y - 1) ? 1 : 0) +
            (HasWall(walls, x, y + 1) ? 1 : 0);

        used.Add(new Point(x, y));

        if (!HasWall(walls, x + 1, y) && !used.Contains(new Point(x + 1, y)))
        {
            wallsC += Rec(walls, x + 1, y, used);
        }
        if (!HasWall(walls, x, y + 1) && !used.Contains(new Point(x, y + 1)))
        {
            wallsC += Rec(walls, x, y + 1, used);
        }

        return wallsC;
    }
    public static void Maze()
    {
        int n = int.Parse(Console.ReadLine());

        var walls = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            var inp = Console.ReadLine();

            for (int j = 0; j < inp.Length; j++)
            {
                if (inp[j] == '#')
                {
                    walls[j, i] = 1;
                }
            }
        }

        Console.WriteLine(Rec(walls, 0, 0, new HashSet<Point>()) * 25);
    }
    //Maze
    public static void Main(string[] args)
    {
        
    }
}