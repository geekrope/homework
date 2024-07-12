//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=48&id_problem=625

using System;
 
public class Program
{
    public static void Main(string[] args)
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
}