//https://acmp.ru/index.asp?main=task&id_task=538

using System;
using System.Collections.Generic;

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


    // RADIO https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=50&id_problem=642
    // memory limit exceeded
    public static bool FrequencesInterfere(Point[] vertices, double distance, out int[] frequences)
    {
        int[] colors = new int[vertices.Length];
        var used = new HashSet<int>();
        var suspended = new Stack<int>();

        while (used.Count < vertices.Length)
        {
            var startingVertex = -1;

            for (int i = 0; i < vertices.Length; i++)
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

                for (int connection = 0; connection < vertices.Length; connection++)
                {
                    double dx = vertices[currentVertex].x - vertices[connection].x;
                    double dy = vertices[currentVertex].y - vertices[connection].y;
                    double currentDistance = Math.Sqrt(dx * dx + dy * dy);

                    if (currentDistance <= distance * 2 && connection != currentVertex)
                    {
                        if (colors[connection] == 0)
                        {
                            colors[connection] = colors[currentVertex] == 1 ? 2 : 1;
                        }
                        else if (colors[connection] != 0 && colors[connection] == colors[currentVertex])
                        {
                            frequences = new int[0] { };
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
        }

        frequences = colors;
        return true;
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
        double right = 1e7;
        int[] frequences = new int[0] { };

        while (right - left > 1e-8)
        {
            var cur = (right + left) / 2;

            if (FrequencesInterfere(vertices, cur, out frequences))
            {
                left = cur;
            }
            else
            {
                right = cur;
            }
        }

        FrequencesInterfere(vertices, left, out frequences);

        Console.WriteLine(left.ToString(System.Globalization.CultureInfo.InvariantCulture));

        var output = "";
        foreach (var frequency in frequences)
        {
            output += frequency + " ";
        }

        Console.WriteLine(output);
    }

    public static void Main(string[] args)
    {
        Radio();
    }
}