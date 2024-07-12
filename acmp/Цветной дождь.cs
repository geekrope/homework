//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=48&id_problem=627

using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());

        List<int>[] connections = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            var split = Console.ReadLine().Split();

            if (connections[i] == null)
            {
                connections[i] = new List<int>();
            }

            for (int j = 0; j <= i; j++)
            {
                if (split[j] == "1")
                {
                    connections[i].Add(j);
                }
            }
        }

        Console.ReadLine();

        int[] colors = new int[n];
        var colInp = Console.ReadLine().Split();

        for (int i = 0; i < n; i++)
        {
            colors[i] = int.Parse(colInp[i]);
        }

        var count = 0;
        for (int i = 0; i < n; i++)
        {
            foreach (var mound in connections[i])
            {
                if (colors[i] != colors[mound])
                {
                    count++;
                }
            }
        }

        Console.WriteLine(count);
    }
}