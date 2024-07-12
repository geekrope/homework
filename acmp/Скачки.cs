//https://acmp.ru/index.asp?main=task&id_task=185

using System;
using System.Collections.Generic;
using System.ComponentModel;

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

    public static void Main(string[] args)
    {
        var inp = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var n = int.Parse(inp[0]);
        var k = int.Parse(inp[1]) - 1;

        List<int>[] connections = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; ; i++)
        {
            var input = Console.ReadLine();
            var connection = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (connection[0] == "0")
            {
                break;
            }

            var first = int.Parse(connection[0]) - 1;
            var second = int.Parse(connection[1]) - 1;

            connections[first].Add(second);
        }

        var count = -1;

        foreach (var vertex in DFS(connections, k))
        {
            count++;
        }

        Console.WriteLine((count == n - 1) ? "Yes" : "No");
    }
}