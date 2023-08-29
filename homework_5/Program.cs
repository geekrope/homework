using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.CompilerServices;

namespace Homework5
{
    public class Program
    {
        //https://acmp.ru/asp/do/index.asp?main=task&id_course=3&id_section=24&id_topic=112&id_problem=687
        public static void Task1() //wrong answer
        {
            var input = Console.ReadLine().Split();

            var A = long.Parse(input[0]);
            var K = long.Parse(input[1]);
            var B = long.Parse(input[2]);
            var M = long.Parse(input[3]);
            var X = long.Parse(input[4]);

            var min = (long)0;
            var max = 2 * X / A; //K=2
            var days = (min + max) / 2;

            while (min + 1 != max)
            {
                var cut = (days - days / K) * A + (days - days / M) * B;

                if (cut < X)
                {
                    min = days;
                }
                else
                {
                    max = days;
                }

                days = (min + max) / 2;
            }

            Console.WriteLine(max);
        }

        //https://acmp.ru/asp/do/index.asp?main=task&id_course=3&id_section=24&id_topic=164&id_problem=1066
        public static void Task2()  //solved
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
                var count = Math.Max((w / (b + 2 * d)) * (h / (a + 2 * d)),  //rotated by 90 degrees 
                    (w / (a + 2 * d)) * (h / (b + 2 * d))); //straight

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

        public static double Root(double arg, int n, double precision)
        {
            var left = 0.0;
            var right = arg;
            var root = (left + right) / 2;

            for (var power = Math.Pow(root, n); Math.Abs(arg - power) > precision; power = Math.Pow(root, n))
            {
                if (power < arg)
                {
                    left = root;
                }
                else if (power > arg)
                {
                    right = root;
                }

                root = (left + right) / 2;
            }

            return root;
        }

        //https://informatics.msk.ru/mod/statements/view.php?id=3516#1
        public static void Task3() //xz
        {
            var arg = double.Parse(Console.ReadLine());
            var n = int.Parse(Console.ReadLine());

            Console.WriteLine(Root(arg, n, 1e-6));
        }

        //https://codeforces.com/problemset/problem/978/C
        public static void Task5() //solved
        {
            var input = Console.ReadLine().Split();

            var n = long.Parse(input[0]);
            var m = long.Parse(input[1]);

            var apartsRaw = Console.ReadLine().Split();
            var aparts = new long[apartsRaw.Length];

            for (int index = 0; index < apartsRaw.Length; index++)
            {
                aparts[index] = long.Parse(apartsRaw[index]);
            }

            var lettersRaw = Console.ReadLine().Split();
            var letters = new long[lettersRaw.Length];

            for (int index = 0; index < lettersRaw.Length; index++)
            {
                letters[index] = long.Parse(lettersRaw[index]);
            }

            var letterNumbersPerApart = new long[aparts.Length + 1]; // 1) apartments 0, 1, 2, 3 ...
            letterNumbersPerApart[1] = 1;

            for (int index = 0; index < aparts.Length; index++)
            {
                letterNumbersPerApart[index + 1] = letterNumbersPerApart[index] + aparts[index];
            }

            foreach (var letter in letters)
            {
                long left = 0;
                long right = aparts.Length + 1;
                long mid = (left + right) / 2;

                while (left + 1 != right)
                {
                    // 2) get letter numbers to mid apartment without including mid apartment 
                    if (letterNumbersPerApart[mid - 1] < letter) 
                    {
                        left = mid;
                    }
                    else
                    {
                        right = mid;
                    }

                    mid = (left + right) / 2;
                }

                // 3) same as 2
                Console.WriteLine(left + " " + Math.Abs(letter - letterNumbersPerApart[left - 1]));
            }
        }

        public static void Main(string[] args)
        {
            Task5();
        }
    }
}