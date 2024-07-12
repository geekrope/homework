//https://acmp.ru/index.asp?main=task&id_task=127

using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static int[] BFS(List<int>[] connections, int startingVertex)
    {
        int[] routes = Enumerable.Repeat(-1, connections.Length).ToArray();
        int[] parents = Enumerable.Repeat(-1, connections.Length).ToArray();
        Queue<int> suspended = new Queue<int>();

        suspended.Enqueue(startingVertex);
        routes[startingVertex] = 0;

        while (suspended.Count > 0)
        {
            var currentVertex = suspended.Dequeue();

            if (routes[currentVertex] == -1)
            {
                routes[currentVertex] = routes[parents[currentVertex]] + 1;
            }

            foreach (var connection in connections[currentVertex])
            {
                if (parents[connection] == -1)
                {
                    parents[connection] = currentVertex;
                    suspended.Enqueue(connection);
                }
            }
        }

        return routes;
    }
    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var connections = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < input.Length; j++)
            {
                if (input[j] == "1")
                {
                    connections[i].Add(j);
                    connections[j].Add(i);
                }
            }
        }

        var target = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var firstTargeted = int.Parse(target[0]) - 1;
        var secondTargeted = int.Parse(target[1]) - 1;
        var routes = BFS(connections, firstTargeted);

        Console.WriteLine(routes[secondTargeted]);
    }
}