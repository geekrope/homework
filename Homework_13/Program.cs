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

    public static void A()
    {
        var inp = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var n = int.Parse(inp[0]);
        var s = int.Parse(inp[1]) - 1;

        List<int>[] connections = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            var connection = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < connection.Length; j++)
            {
                if (connection[j] == "1")
                {
                    connections[i].Add(j);
                }
            }
        }

        var count = -1;

        foreach (var vertex in DFS(connections, s))
        {
            count++;
        }

        Console.WriteLine(count);
    }
    public static void B()
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
    public static void C()
    {
        var n = int.Parse(Console.ReadLine());

        List<int>[] connections = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            var connection = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < connection.Length; j++)
            {
                if (connection[j] == "1")
                {
                    connections[i].Add(j);
                }
            }
        }

        Console.WriteLine(IsTree(connections) ? "YES" : "NO");
    }
    public static void D()
    {
        var inp = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var n = int.Parse(inp[0]);
        var m = int.Parse(inp[1]);

        List<int>[] connections = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < m; i++)
        {
            var connection = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var first = int.Parse(connection[0]) - 1;
            var second = int.Parse(connection[1]) - 1;

            connections[first].Add(second);
            connections[second].Add(first);
        }

        Console.WriteLine(IsSeparable(connections) ? "YES" : "NO");
    }
    public static void E()
    {
        var inp = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var n = int.Parse(inp[0]);
        var m = int.Parse(inp[1]);

        List<int>[] connections = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < m; i++)
        {
            var connection = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var first = int.Parse(connection[0]) - 1;
            var second = int.Parse(connection[1]) - 1;

            connections[first].Add(second);
            connections[second].Add(first);
        }

        var comps = FindComponents(connections);

        Console.WriteLine(comps.Count);

        foreach (var comp in comps)
        {
            Console.WriteLine(comp.Count);

            var output = "";

            foreach (var el in comp)
            {
                output += (el + 1) + " ";
            }

            Console.WriteLine(output);
        }
    } // memory limit
    public static void F()
    {
        var inp = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var n = int.Parse(inp[0]);
        var m = int.Parse(inp[1]);
        var matrix = new bool[n, m];
        var dots = 0;

        List<int>[] connections = new List<int>[n * m];

        for (int i = 0; i < n * m; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            var remains = Console.ReadLine();

            for (int j = 0; j < remains.Length; j++)
            {
                if (remains[j] == '#')
                {
                    matrix[i, j] = true;
                }
                else
                {
                    dots++;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (matrix[i, j])
                {
                    if (i - 1 >= 0 && matrix[i - 1, j])
                    {
                        connections[i * m + j].Add((i - 1) * m + j);
                    }
                    if (j - 1 >= 0 && matrix[i, j - 1])
                    {
                        connections[i * m + j].Add(i * m + j - 1);
                    }
                    if (i + 1 < n && matrix[i + 1, j])
                    {
                        connections[i * m + j].Add((i + 1) * m + j);
                    }
                    if (j + 1 < m && matrix[i, j + 1])
                    {
                        connections[i * m + j].Add(i * m + j + 1);
                    }
                }
            }
        }

        Console.WriteLine(FindComponents(connections).Count - dots);
    } // time limit
    public static void Main(string[] args)
    {
        F();
    }
}