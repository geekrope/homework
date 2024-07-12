//https://acmp.ru/index.asp?main=task&id_task=909

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;

class Vertex
{
    public int Index;
    public bool Fictional;

    public Vertex(int index, bool fictional)
    {
        Index = index;
        Fictional = fictional;
    }
}

class Closure
{
    public int onePiece = 0;
    public int damaged = 0;
    public int destroyed = 0;
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
    public static void FindComponents(List<int>[] connections, Action<List<int>> action)
    {
        HashSet<int> used = new HashSet<int>();
        var component = new List<int>();

        while (used.Count != connections.Length)
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

            foreach (var vertex in DFS(connections, startingVertex))
            {
                used.Add(vertex);
                component.Add(vertex);
            }

            action(component);
            component.Clear();
        }
    }
    public static int[] BFSF(List<int>[] connections, char[] terrain, int startingVertex)
    {
        int[] routes = Enumerable.Repeat(-1, connections.Length).ToArray();
        int[] parents = Enumerable.Repeat(-1, connections.Length).ToArray();
        Queue<Vertex> suspended = new Queue<Vertex>();

        suspended.Enqueue(new Vertex(startingVertex, false));
        routes[startingVertex] = 0;

        while (suspended.Count > 0)
        {
            var currentVertex = suspended.Dequeue();

            if (currentVertex.Fictional)
            {
                currentVertex.Fictional = false;
                suspended.Enqueue(currentVertex);
            }
            else
            {
                if (routes[currentVertex.Index] == -1)
                {
                    var weight = terrain[currentVertex.Index] == 'W' ? 2 : 1;
                    routes[currentVertex.Index] = routes[parents[currentVertex.Index]] + weight;
                }

                foreach (var connection in connections[currentVertex.Index])
                {
                    if (parents[connection] == -1)
                    {
                        parents[connection] = currentVertex.Index;

                        if (terrain[connection] == 'W')
                        {
                            suspended.Enqueue(new Vertex(connection, true));
                        }
                        else
                        {
                            suspended.Enqueue(new Vertex(connection, false));
                        }
                    }
                }
            }
        }

        return routes;
    }
    public static int[] BFS(Func<int, int[]> connections, int total, int startingVertex)
    {
        int[] routes = Enumerable.Repeat(-1, total).ToArray();
        int[] parents = Enumerable.Repeat(-1, total).ToArray();
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

            foreach (var connection in connections(currentVertex))
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
        var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var n = int.Parse(input[0]);
        var m = int.Parse(input[1]);

        var matrix = new char[n, m];

        for (var i = 0; i < n; i++)
        {
            var row = Console.ReadLine();

            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = row[j];
            }
        }

        var connections = new List<int>[n * m];

        for (int i = 0; i < n * m; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (matrix[i, j] == 'S' || matrix[i, j] == 'X')
                {
                    for (int o1 = -1; o1 <= 1; o1++)
                    {
                        for (int o2 = -1; o2 <= 1; o2++)
                        {
                            var iN = o1 + i;
                            var jN = o2 + j;

                            if (o1 * o1 + o2 * o2 == 1 && iN >= 0 && jN >= 0 && iN < n && jN < m && (matrix[iN, jN] == 'S' || matrix[iN, jN] == 'X'))
                            {
                                connections[i * m + j].Add(iN * m + jN);
                            }
                        }
                    }
                }
            }
        }

        var answ = new Closure();

        FindComponents(connections, (comp) =>
        {
            if (comp.Count > 1 || matrix[comp[0] / m, comp[0] % m] == 'S' || matrix[comp[0] / m, comp[0] % m] == 'X')
            {
                var damagedVerts = 0;

                foreach (var vert in comp)
                {
                    var i = vert / m;
                    var j = vert % m;

                    if (matrix[i, j] == 'X')
                    {
                        damagedVerts++;
                    }
                }

                if (damagedVerts == 0)
                {
                    answ.onePiece++;
                }
                else if (damagedVerts == comp.Count)
                {
                    answ.destroyed++;
                }
                else
                {
                    answ.damaged++;
                }
            }
        });

        Console.WriteLine(answ.onePiece + " " + answ.damaged + " " + answ.destroyed);
    }
}