//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=50&id_problem=1013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class Program
{
    public static HashSet<int> DFSRecursive(List<int>[] connections, HashSet<int> used, int startingVertex, Action<int> action)
    {
        if (!used.Contains(startingVertex))
        {
            used.Add(startingVertex);

            foreach (var connection in connections[startingVertex])
            {
                DFSRecursive(connections, used, connection, action);
            }

            action(startingVertex);
        }

        return used;
    }
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
    public static IEnumerable<int> DFS(Dictionary<int, List<int>> connections, int startingVertex)
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
    public static List<List<int>> FindComponents(List<int>[] connections)
    {
        HashSet<int> used = new HashSet<int>();
        List<List<int>> components = new List<List<int>>();

        while (used.Count != connections.Length)
        {
            var component = new List<int>();
            var startingVertex = -1;

            for (int i = 0; i < connections.Length; i++)
            {
                if (!used.Contains(i))
                {
                    startingVertex = i;
                    break;
                }
            }

            foreach (var vertex in DFS(connections, startingVertex))
            {
                used.Add(vertex);
                component.Add(vertex);
            }

            components.Add(component);
        }

        return components;
    }
    public static List<List<int>> FindComponents(Dictionary<int, List<int>> connections)
    {
        HashSet<int> used = new HashSet<int>();
        List<List<int>> components = new List<List<int>>();

        while (used.Count != connections.Count)
        {
            var component = new List<int>();
            var startingVertex = -1;

            foreach (var vertex in connections)
            {
                if (!used.Contains(vertex.Key))
                {
                    startingVertex = vertex.Key;
                    break;
                }
            }

            foreach (var vertex in DFS(connections, startingVertex))
            {
                used.Add(vertex);
                component.Add(vertex);
            }

            components.Add(component);
        }

        return components;
    }
    public static bool IsSeparable(List<int>[] connections)
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

        return true;
    }
    public static bool IsTree(List<int>[] connections)
    {
        var used = new HashSet<int>();
        var suspended = new Stack<int>();
        var connectionsCount = 0;

        foreach (var vertex in connections)
        {
            foreach (var connection in vertex)
            {
                connectionsCount++;
            }
        }

        suspended.Push(0);
        used.Add(0);

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
        }

        return (connectionsCount / 2 == connections.Length - 1) && (used.Count == connections.Length);
    }

    public static void Main(string[] args)
    {
        var inp = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var n = int.Parse(inp[0]);
        var m = int.Parse(inp[1]);
        var matrix = new string[n];

        Dictionary<int, List<int>> connections = new Dictionary<int, List<int>>();

        for (int i = 0; i < n; i++)
        {
            matrix[i] = Console.ReadLine();

            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] == '#')
                {
                    connections.Add(i * m + j, new List<int>());
                }
            }
        }

        foreach (var vert in connections.Keys)
        {
            var i = vert / m;
            var j = vert % m;
            if (i - 1 >= 0 && matrix[i - 1][j] == '#')
            {
                connections[i * m + j].Add((i - 1) * m + j);
            }
            if (j - 1 >= 0 && matrix[i][j - 1] == '#')
            {
                connections[i * m + j].Add(i * m + j - 1);
            }
            if (i + 1 < n && matrix[i + 1][j] == '#')
            {
                connections[i * m + j].Add((i + 1) * m + j);
            }
            if (j + 1 < m && matrix[i][j + 1] == '#')
            {
                connections[i * m + j].Add(i * m + j + 1);
            }
        }

        Console.WriteLine(FindComponents(connections).Count);
    }
}