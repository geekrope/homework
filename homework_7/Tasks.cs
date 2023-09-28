using homework_7;
using System.Security.Cryptography.X509Certificates;

namespace Homework5
{
    public class Tasks
    {
        public static int Gcd(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return Gcd(b, a % b);
            }
        }

        public static void A()
        {
            var input = Console.ReadLine().Split();

            var a = int.Parse(input[0]);
            var b = int.Parse(input[1]);
            var c = int.Parse(input[2]);
            var d = int.Parse(input[3]);

            var numerator = (a * d + c * b);
            var denominator = (b * d);
            var gcd = Gcd(numerator, denominator);

            Console.WriteLine((numerator / gcd) + "/" + (denominator / gcd));
        }

        public static int Sqrt(int num)
        {
            int sqrt = 0;

            for (int x = 0; x * x <= num; x++)
            {
                sqrt = x;
            }

            return sqrt;
        }

        public static void D()
        {
            var number = int.Parse(Console.ReadLine());
            var output = "";

            for (int i = 0; i < 4; i++)
            {
                var sqrt = Sqrt(number);

                if (sqrt > 0)
                {
                    output += sqrt + " ";
                }
                else
                {
                    break;
                }

                number -= sqrt * sqrt;
            }

            Console.WriteLine(output);
        }

        public static int Pow3(int num)
        {
            return num * num * num;
        }

        public static int Cbrt(int num)
        {
            int cbrt = 0;

            for (int x = 0; Pow3(x) <= num; x++)
            {
                cbrt = x;
            }

            return cbrt;
        }

        public static void E()
        {
            var number = int.Parse(Console.ReadLine());

            var cbrt1 = Cbrt(number);
            var cbrt2 = Cbrt(number - Pow3(cbrt1));

            if (Pow3(cbrt1) + Pow3(cbrt2) == number)
            {
                Console.WriteLine($"{cbrt1} {cbrt2}");
            }
            else
            {
                Console.WriteLine("impossible");
            }
        }

        public static long FindFactorsSum(int n)
        {
            long sum = 0;

            for (int i = 1; i < n; i++)
            {
                if (n % i == 0)
                {
                    sum += i;
                }
            }

            return sum;
        }

        public static void F()
        {
            var sums = new Dictionary<long, List<int>>();

            var k = int.Parse(Console.ReadLine());

            for (int i = 0; i < k; i++)
            {
                var sum = FindFactorsSum(i);

                if (!sums.ContainsKey(sum))
                {
                    sums.Add(sum, new List<int>() { i });
                }
                else
                {
                    sums[sum].Add(i);
                }

                if (sums.ContainsKey(i))
                {
                    foreach (var num in sums[i])
                    {
                        if (sum == num && i != num)
                        {
                            Console.WriteLine($"{i} {num}");
                        }
                    }
                }
            }
        }

        public static bool IsPrimes(int n)
        {
            var primes = new bool[n + 1];
            Array.Fill(primes, true);

            primes[0] = primes[1] = false;

            for (int i = 2; i < n; i++)
            {
                if ((long)i * i <= n)
                {
                    for (var j = (long)i * i; j <= n; j += i)
                    {
                        primes[j] = false;
                    }
                }
            }

            return primes[n];
        }

        public static bool[] FindPrimes(int n)
        {
            var primes = new bool[n + 1];
            Array.Fill(primes, true);

            primes[0] = primes[1] = false;

            for (int i = 2; i < n; i++)
            {
                if ((long)i * i <= n)
                {
                    for (var j = (long)i * i; j <= n; j += i)
                    {
                        primes[j] = false;
                    }
                }
            }

            return primes;
        }

        public static int[] FindPrimes(int n, bool[] primes)
        {
            var primesList = new List<int>();

            for (int i = 0; i < primes.Length; i++)
            {
                if (primes[i])
                {
                    primesList.Add(i);
                }
            }

            return primesList.ToArray();
        }

        public static void G()
        {
            var n = int.Parse(Console.ReadLine());
            var primes = FindPrimes(n);
            var exactPrimes = FindPrimes(n, primes);

            foreach (var prime in exactPrimes)
            {
                var dif = n - prime;

                if (primes[dif])
                {
                    Console.WriteLine($"{dif} {prime}");
                    return;
                }
            }
        }

        public static void H()
        {
            var n = int.Parse(Console.ReadLine());
            var primes = FindPrimes(n);

            Console.WriteLine(primes[n] ? "prime" : "composite");
        }

        public static void Main(string[] args)
        {
            List<Profile> profiles = new List<Profile>
            {
                new Vacancy("C# разработчик", 100000),
                new Vacancy("Python разработчик", 90000),
                new Vacancy("C++ разработчик", 110000),
                new Resume("C# разработчик", 4),
                new Resume("C++ разработчик", 1),
            };

            foreach (var profile in profiles)
            {
                Console.WriteLine(profile.Describe());
            }
        }
    }
}