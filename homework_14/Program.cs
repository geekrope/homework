using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;

public struct Point
{
    public int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Program
{
    public static IEnumerable<int> DFS(List<int>[] connections, int startingVertex)
    {
        var used = new HashSet<int>();
        var suspended = new Stack<int>();

        suspended.Push(startingVertex);
        used.Add(startingVertex);

        while (suspended.Count > 0)
        {
            var currentVertex = suspended.Pop();

            foreach (var connection in connections[currentVertex])
            {
                if (!used.Contains(connection))
                {
                    suspended.Push(connection);
                    used.Add(connection);
                }
            }

            yield return currentVertex;
        }
    }
    public static bool IsSeparable(List<int>[] connections, out int[] res)
    {
        int[] colors = new int[connections.Length];
        var used = new HashSet<int>();
        var suspended = new Stack<int>();

        while (used.Count < connections.Length)
        {
            var startingVertex = -1;
            for (int i = 0; i < connections.Length; i++)
            {
                if (!used.Contains(i))
                {
                    startingVertex = i;
                    break;
                }
            }

            colors[startingVertex] = 1;
            suspended.Push(startingVertex);
            used.Add(startingVertex);

            while (suspended.Count > 0)
            {
                var currentVertex = suspended.Pop();

                foreach (var connection in connections[currentVertex])
                {
                    if (colors[connection] == 0)
                    {
                        colors[connection] = colors[currentVertex] == 1 ? 2 : 1;
                    }
                    else if (colors[connection] != 0 && colors[connection] == colors[currentVertex])
                    {
                        res = new int[0] { };
                        return false;
                    }

                    if (!used.Contains(connection))
                    {
                        suspended.Push(connection);
                        used.Add(connection);
                    }
                }
            }
        }

        res = colors;
        return true;
    }

    // RADIO https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=50&id_problem=642
    // memory limit exceeded
    public static List<int>[] ConnectVerticesWithinDistance(Point[] vertices, double distance)
    {
        var length = vertices.Length;
        var connections = new List<int>[length];

        for (int i = 0; i < length; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                double dx = vertices[j].x - vertices[i].x;
                double dy = vertices[j].y - vertices[i].y;
                double currentDistance = Math.Sqrt(dx * dx + dy * dy);

                if (currentDistance < distance * 2 && i != j)
                {
                    connections[i].Add(j);
                    connections[j].Add(i);
                }
            }
        }

        return connections;
    }
    public static void Radio()
    {
        var n = int.Parse(Console.ReadLine());
        var vertices = new Point[n];

        for (int i = 0; i < n; i++)
        {
            var xy = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var x = int.Parse(xy[0]);
            var y = int.Parse(xy[1]);

            vertices[i] = new Point(x, y);
        }

        double left = 0;
        double right = 1e9;
        int[] colors = new int[0] { };

        while (right - left > 1e-8)
        {
            var cur = (right + left) / 2;

            if (IsSeparable(ConnectVerticesWithinDistance(vertices, cur), out colors))
            {
                left = cur;
            }
            else
            {
                right = cur;
            }
        }

        IsSeparable(ConnectVerticesWithinDistance(vertices, left), out colors);

        Console.WriteLine(left.ToString(System.Globalization.CultureInfo.InvariantCulture));
        var o = "";
        foreach (var color in colors)
        {
            o += color + " ";
        }
        Console.WriteLine(o);
    }

    public static void Main(string[] args)
    {
        Radio();
    }
}