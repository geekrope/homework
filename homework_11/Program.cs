using System;
using System.Linq;

namespace homework_11
{
    public class Agent
    {
        public int Age, RevealRisk;

        public Agent(int age, int revealRisk)
        {
            Age = age;
            RevealRisk = revealRisk;
        }
    }
    public class Program
    {
        public static void A()
        {
            var n = int.Parse(Console.ReadLine());
            var slices = new int[n + 1];

            slices[0] = 1;

            for (int i = 1; i <= n; i++)
            {
                slices[i] = slices[i - 1] + i;
            }

            Console.WriteLine(slices[n]);
        }   
        public static void B()
        {
            var n = int.Parse(Console.ReadLine());
            var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var nails = new int[input.Length];
            var d = new int[nails.Length];

            for (int i = 0; i < input.Length; i++)
            {
                nails[i] = int.Parse(input[i]);
            }

            Array.Sort(nails);

            d[0] = nails[1] - nails[0];
            d[1] = nails[2] - nails[0];

            for (int i = 2; i < nails.Length; i++)
            {
                d[i] = Math.Min(d[i - 2], d[i - 1]) + nails[i] - nails[i - 1];
            }

            Console.WriteLine(d[n-1]);
        }
        public static void C()
        {
            var n = int.Parse(Console.ReadLine());
            var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var agents = new Agent[input.Length / 2];

            for (int i = 0; i < input.Length - 1; i += 2)
            {
                agents[i / 2] = new Agent(int.Parse(input[i]), int.Parse(input[i + 1]));
            }

            Array.Sort(agents, delegate (Agent x, Agent y) { return x.Age.CompareTo(y.Age); });

            long reveal1 = 0;
            long reveal2 = 0;

            for (int i = 0; i < agents.Length - 1; i += 2)
            {
                reveal1 += agents[i + 1].RevealRisk;
            }
            if (agents.Length % 2 != 0)
            {
                reveal1 += agents[agents.Length - 1].RevealRisk;
            }
            for (int i = agents.Length - 1; i >= 0; i -= 2)
            {
                reveal2 += agents[i].RevealRisk;
            }

            Console.WriteLine(Math.Min(reveal1, reveal2));
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
        public static void D()
        {
            var input = Console.ReadLine().Split();
            var n = int.Parse(input[0]);
            var k = int.Parse(input[1]);

            Console.WriteLine(DFunc(0, n, k, false));
        }
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
        public static void Main(string[] args)
        {
            B2();
        }
    }
}