using System;
using System.Collections.Generic;

public class Program
{
    //https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=48&id_problem=625
    public static void A()
    {
        int n = 0;
        int count = 0;

        n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            var split = Console.ReadLine().Split();

            for (int j = 0; j <= i; j++)
            {
                if (split[j] == "1")
                {
                    count++;
                }
            }
        }

        Console.WriteLine(count);
    }
    //https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=48&id_problem=626
    public static void B()
    {
        var input = Console.ReadLine().Split();
        int n = int.Parse(input[0]);
        int m = int.Parse(input[1]);

        int[] tunnels = new int[n];

        for (int i = 0; i < m; i++)
        {
            var split = Console.ReadLine().Split();

            var f = int.Parse(split[0]);
            var s = int.Parse(split[1]);

            tunnels[f - 1]++;
            tunnels[s - 1]++;
        }

        Console.WriteLine(string.Join(" ", tunnels));
    }
    //https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=48&id_problem=627
    public static void C()
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
    public static void Main(string[] args)
    {
        C();
    }
}