using System;
using System.Collections.Generic;

public class Program
{
    public static HashSet<int> DFS(List<int>[] joints, HashSet<int> cameAcross, int startingVertex, Action<int> action)
    {
        if (!cameAcross.Contains(startingVertex))
        {
            cameAcross.Add(startingVertex);

            foreach (var joint in joints[startingVertex])
            {
                DFS(joints, cameAcross, joint, action);
            }

            action(startingVertex);
        }

        return cameAcross;
    }
    public static List<List<int>> FindComponents(List<int>[] joints)
    {
        HashSet<int> cameAcross = new HashSet<int>();
        List<List<int>> components = new List<List<int>>();

        while (cameAcross.Count != joints.Length)
        {
            var component = new List<int>();
            var startingVertex = -1;

            for (int i = 0; i < joints.Length; i++)
            {
                if (!cameAcross.Contains(i))
                {
                    startingVertex = i;
                    break;
                }
            }

            DFS(joints, cameAcross, startingVertex, (int vertex) => { component.Add(vertex); });

            components.Add(component);
        }

        return components;
    }
    public static void E()
    {
        var inp = Console.ReadLine().Split();
        var n = int.Parse(inp[0]);
        var m = int.Parse(inp[1]);

        List<int>[] joints = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            joints[i] = new List<int>();
        }

        for (int i = 0; i < m; i++)
        {
            var joint = Console.ReadLine().Split();
            var first = int.Parse(joint[0]) - 1;
            var second = int.Parse(joint[1]) - 1;

            joints[first].Add(second);
            joints[second].Add(first);
        }

        var comps = FindComponents(joints);

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
    }
    public static void Main(string[] args)
    {
        E();
    }
}