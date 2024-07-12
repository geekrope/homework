//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=48&id_problem=626

using System;

public class Program
{    
    public static void Main(string[] args)
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
}