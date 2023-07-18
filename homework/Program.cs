using System;
using System.Collections.Generic;

namespace Homework1
{
    public struct SubstringBounds
    {
        public int? Start, End;
    }

    public struct Triplet
    {
        public int a, b, c;
    }

    public static class Program
    {
        //https://acmp.ru/asp/do/index.asp?main=task&id_course=1&id_section=9&id_topic=123&id_problem=764
        public static void Task1()
        {
            string count = Console.ReadLine();
            string input = Console.ReadLine();

            string[] splittedInput = input.Split(' ');
            string output = "";

            for (int index = splittedInput.Length - 1; index >= 0; index--)
            {
                output += splittedInput[index] + " ";
            }

            Console.WriteLine(output);
        }

        //https://acmp.ru/asp/do/index.asp?main=task&id_course=1&id_section=9&id_topic=123&id_problem=779
        public static void Task2()
        {
            int count = int.Parse(Console.ReadLine()) + 1;

            if (count == 1)
            {
                Console.WriteLine(0);
            }
            else
            {
                int[] sequence = new int[count];

                sequence[0] = 0;
                sequence[1] = 1;

                for (int index = 2; index < count; index++)
                {
                    sequence[index] = sequence[index - 1] + sequence[index - 2];
                }

                Console.WriteLine(sequence[sequence.Length - 1]);
            }
        }

        //https://acmp.ru/asp/do/index.asp?main=task&id_course=1&id_section=9&id_topic=123&id_problem=769
        public static void Task3()
        {
            var cache = new Dictionary<Triplet, int>();
            var input = Console.ReadLine().Split(' ');

            var a = int.Parse(input[0]);
            var b = int.Parse(input[1]);
            var c = int.Parse(input[2]);

            Console.WriteLine(Task3Func(a, b, c, cache));
        }

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

        //https://acmp.ru/asp/do/index.asp?main=task&id_course=3&id_section=24&id_topic=106&id_problem=555
        public static void Task4()
        {
            var count = int.Parse(Console.ReadLine());
            var schools = new Dictionary<int, int>();

            for (int index = 0; index < count; index++)
            {
                var school = Console.ReadLine();
                var schoolNumber = FindNumber(school);

                if (schools.ContainsKey(schoolNumber))
                {
                    schools[schoolNumber]++;
                }
                else
                {
                    schools[schoolNumber] = 1;
                }
            }

            PrintTask4(FindSuitableSchools(schools));
        }

        public static SubstringBounds FindNumberBounds(string input)
        {
            var digits = new HashSet<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int? start = null;
            int? end = null;

            for (int index = 0; index < input.Length; index++)
            {
                var isDigit = digits.Contains(input[index]);

                if (start == null && isDigit)
                {
                    start = index;
                }
                else if (!isDigit && start != null && end == null)
                {
                    end = index;
                }
            }

            return new SubstringBounds() { Start = start, End = end };
        }

        public static int FindNumber(string input)
        {
            var bounds = FindNumberBounds(input);

            if (bounds.Start.HasValue)
            {
                if (bounds.End.HasValue)
                {
                    return int.Parse(input.Substring(bounds.Start.Value, bounds.End.Value - bounds.Start.Value));
                }
                else
                {
                    return int.Parse(input.Substring(bounds.Start.Value));
                }
            }
            else
            {
                return -1;
            }
        }

        public static List<int> FindSuitableSchools(Dictionary<int, int> schools)
        {
            var suitableSchools = new List<int>();

            foreach (var school in schools)
            {
                if (school.Value >= 1 && school.Value <= 5)
                {
                    suitableSchools.Add(school.Key);
                }
            }

            return suitableSchools;
        }

        public static void PrintTask4(List<int> suitableSchools)
        {
            Console.WriteLine(suitableSchools.Count);

            foreach (var school in suitableSchools)
            {
                Console.WriteLine(school);
            }
        }

        //https://acmp.ru/asp/do/index.asp?main=topic&id_course=3&id_section=24&id_topic=164
        public static void Task5()
        {
            int n = int.Parse(Console.ReadLine());
            string[] inputSplitted = Console.ReadLine().Split(' ');
            int[] prizes = new int[inputSplitted.Length];

            for (int index = 0; index < inputSplitted.Length; index++)
            {
                prizes[index] = int.Parse(inputSplitted[index]);
            }

            var output = "";

            for (int k = 2; k <= n; k++)
            {
                var max = FindMaxUpToK(prizes, k, -1);
                var previousMax = FindMaxUpToK(prizes, k, Array.IndexOf(prizes, max));

                output += previousMax + " ";
            }

            Console.WriteLine(output);
        }

        public static int FindMaxUpToK(int[] numbers, int k, int exclude)
        {
            var max = -1;

            for (int index = 0; index < k; index++)
            {
                var number = numbers[index];

                if (number > max && index != exclude)
                {
                    max = number;
                }
            }

            return max;
        }

        public static void Main(string[] args)
        {
            Task5();
        }
    }
}