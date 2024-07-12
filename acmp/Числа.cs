//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=51&id_problem=1015

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
    public static List<int> BFSC(int startingNumber, int target)
    {
        Dictionary<int, int> parents = new Dictionary<int, int>();
        Queue<int> suspended = new Queue<int>();

        suspended.Enqueue(startingNumber);

        while (!parents.ContainsKey(target))
        {
            var currentNumber = suspended.Dequeue();

            var furtherNumbers = new List<int>() { (currentNumber % 1000) * 10 + currentNumber / 1000, (currentNumber % 10) * 1000 + currentNumber / 10 };

            if (currentNumber / 1000 != 9)
            {
                furtherNumbers.Add(currentNumber + 1000);
            }
            if (currentNumber % 10 != 1)
            {
                furtherNumbers.Add(currentNumber - 1);
            }

            foreach (var further in furtherNumbers)
            {
                if (!parents.ContainsKey(further))
                {
                    parents[further] = currentNumber;
                    suspended.Enqueue(further);
                }
            }
        }

        var route = new List<int>();
        var currentNode = target;
        route.Add(currentNode);

        while (route[route.Count - 1] != startingNumber)
        {
            currentNode = parents[currentNode];
            route.Add(currentNode);
        }

        route.Reverse();

        return route;
    }

    //https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=51&id_problem=649
    public static void A()
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

    public static void Main(string[] args)
    {
        var first = int.Parse(Console.ReadLine());
        var second = int.Parse(Console.ReadLine());

        foreach (var node in BFSC(first, second))
        {
            Console.WriteLine(node);
        }
    }
}