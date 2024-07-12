//https://acmp.ru/asp/do/index.asp?main=task&id_course=1&id_section=3&id_topic=37&id_problem=215

using System;
using System.Runtime.CompilerServices;
 
namespace Homework5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();

            var n = long.Parse(input[0]);
            var a = long.Parse(input[1]);
            var b = long.Parse(input[2]);
            var w = long.Parse(input[3]);
            var h = long.Parse(input[4]);

            var min = (long)0;
            var max = Math.Max(w, h);
            var d = (min + max) / 2;

            while (min + 1 != max)
            {
                var count = Math.Max((w / (b + 2 * d)) * (h / (a + 2 * d)), (w / (a + 2 * d)) * (h / (b + 2 * d)));

                if (count < n)
                {
                    max = d;
                }
                else
                {
                    min = d;
                }

                d = (min + max) / 2;
            }

            Console.WriteLine(min);
        }
    }
}