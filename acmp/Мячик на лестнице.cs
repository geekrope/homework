//https://acmp.ru/index.asp?main=task&id_task=544

using System;
using System.Linq;

namespace homework_11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var routes = new long[Math.Max(3, n + 1)];
            routes[0] = 1;
            routes[1] = 1;
            routes[2] = 2;

            for (int i = 3; i <= n; i++)
            {
                routes[i] = routes[i - 1] + routes[i - 2] + routes[i - 3];
            }

            Console.WriteLine(routes[n]);
        }
    }
}