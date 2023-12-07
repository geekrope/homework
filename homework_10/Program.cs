using System;
using System.Linq;

namespace homework_10
{
    public struct Triplet
    {
        public int a, b, c;

        public Triplet(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
    public class Program
    {
        private static int f(int number, int a, int b, int c, Dictionary<Triplet, int> prevExecutions)
        {
            if (prevExecutions.ContainsKey(new Triplet(a, b, c)))
            {
                return prevExecutions[new Triplet(a, b, c)];
            }
            else
            {
                int result = number;

                result = a > 0 ? Math.Min(result, f(number / 2, a - 1, b, c, prevExecutions)) : result;
                result = b > 0 ? Math.Min(result, f((number + 1) / 2, a, b - 1, c, prevExecutions)) : result;
                result = c > 0 ? Math.Min(result, f((number - 1) / 2, a, b, c - 1, prevExecutions)) : result;

                prevExecutions.Add(new Triplet(a, b, c), result);

                return result;
            }
        }
        private static void Task1()
        {
            var input = Console.ReadLine();
            var split = input.Split();
            var n = int.Parse(split[0]);
            var a = int.Parse(split[1]);
            var b = int.Parse(split[2]);
            var c = int.Parse(split[3]);

            Console.WriteLine(f(n, a, b, c, new Dictionary<Triplet, int>()));
        }

        private static long gcd(long a, long b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return gcd(b, a % b);
            }
        }
        private static long lcm(long a, long b)
        {
            return a * b / gcd(a, b);
        }
        private static void Task2()
        {
            var q = long.Parse(Console.ReadLine());

            for (long i = 0; i < q; i++)
            {
                var n = int.Parse(Console.ReadLine());
                var pInp = Console.ReadLine().Split();
                long[] p = new long[n];
                for (long j = 0; j < pInp.Length; j++)
                {
                    p[j] = long.Parse(pInp[j]);
                }
                var inp1 = Console.ReadLine().Split();
                var x = long.Parse(inp1[0]);
                var a = long.Parse(inp1[0]);
                var inp2 = Console.ReadLine().Split();
                var y = long.Parse(inp2[0]);
                var b = long.Parse(inp2[0]);
                var k = long.Parse(Console.ReadLine());

                var z = y + x;
                var c = lcm(a, b);


                Array.Sort(p);

                var left = -1;
                var right = n;

                while (left + 1 != right)
                {
                    var mid = (left + right) / 2;

                    long earned = 0;
                    var count = 0;

                    if (x > y)
                    {
                        var t = y;
                        y = x;
                        x = t;

                        t = a;
                        a = b;
                        b = t;
                    }

                    for (var l = 0; count < mid && l < n / c; l++, count++)
                    {
                        earned += (z * p[n - l - 1]) / 100;
                    }
                    for (var l = 0; count < mid && l < n / b; l++, count++)
                    {
                        earned += (y * p[n - l - 1]) / 100;
                    }
                    for (var l = 0; count < mid && l < n / a; l++, count++)
                    {
                        earned += (x * p[n - l - 1]) / 100;
                    }

                    if (earned > k)
                    {
                        right = mid;
                    }
                    else if (earned < k)
                    {
                        left = mid;
                    }
                    else
                    {
                        left = right = mid;
                    }
                }

                Console.WriteLine(right);
            }
        }

        public static void Main(string[] args)
        {
            Task2();
        }
    }
}