using System;
using System.Linq;

namespace homework_11
{
    public class Program
    {
        public static void E()
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
        public static int DFunc(int i, int n, int k, bool zero)
        {
            if (i == n - 1)
            {
                if (zero)
                {
                    return k - 1;
                }
                else
                {
                    return k;
                }
            }
            else
            {
                var withoutZero = (k - 1) * DFunc(i + 1, n, k, false);
                var withZero = (zero || i == 0) ? 0 : DFunc(i + 1, n, k, true);
                return withoutZero + withZero;
            }
        }
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();
            var n = int.Parse(input[0]);
            var k = int.Parse(input[1]);

            Console.WriteLine(DFunc(0, n, k, false));
        }
    }
}