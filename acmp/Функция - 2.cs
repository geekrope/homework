//https://acmp.ru/asp/do/index.asp?main=task&id_course=1&id_section=9&id_topic=123&id_problem=769

using System;
using System.Collections.Generic;

namespace Homework1
{
    public struct Triplet
    {
        public int a, b, c;
    }

    public static class Program
    {
        public static int Task3Func(int a, int b, int c, Dictionary<Triplet, int> cache)
        {
            var triplet = new Triplet() { a = a, b = b, c = c };

            if (cache.ContainsKey(triplet))
            {
                return cache[triplet];
            }
            else
            {
                int value;

                if (a <= 0 || b <= 0 || c <= 0)
                {
                    value = 1;
                }
                else if (a > 20 || b > 20 || c > 20)
                {
                    value = Task3Func(20, 20, 20, cache);
                }
                else if (a < b && b < c)
                {
                    value = Task3Func(a, b, c - 1, cache) + Task3Func(a, b - 1, c - 1, cache) - Task3Func(a, b - 1, c, cache);
                }
                else
                {
                    value = Task3Func(a - 1, b, c, cache) + Task3Func(a - 1, b - 1, c, cache) + Task3Func(a - 1, b, c - 1, cache) - Task3Func(a - 1, b - 1, c - 1, cache);
                }

                cache.Add(triplet, value);
                return value;
            }
        }

        public static void Main(string[] args)
        {
            var cache = new Dictionary<Triplet, int>();
            var input = Console.ReadLine().Split();

            var a = int.Parse(input[0]);
            var b = int.Parse(input[1]);
            var c = int.Parse(input[2]);

            Console.WriteLine(Task3Func(a, b, c, cache));
        }
    }
}