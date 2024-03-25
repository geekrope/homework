using System;
using System.Collections.Generic;

namespace homework_18
{
    public class Program
    {
        public static int[] FindMinMax(int[] array, int sampleLength, Func<int, int, int> comparator, int defaultValue)
        {
            List<int> maxes = new List<int>();

            var max = defaultValue;

            for (int i = 0; i < array.Length; i++)
            {
                if (i % sampleLength == 0 && i != 0)
                {
                    maxes.Add(max);
                    max = defaultValue;
                }

                max = comparator(max, array[i]);
            }

            if (array.Length % sampleLength != 0)
            {
                maxes.Add(max);
            }

            return maxes.ToArray();
        }
        public static int FindMinMax(int[] array, int[] maxes, int sampleLength, int start, int end, Func<int, int, int> comparator, int defaultValue)
        {
            var max = defaultValue;

            if (((start + sampleLength - 1) / sampleLength) * sampleLength > array.Length || end - start < sampleLength)
            {
                for (var i = start; i <= end; i++)
                {
                    max = comparator(max, array[i]);
                }
            }
            else
            {
                while (start % sampleLength != 0)
                {
                    max = comparator(max, array[start]);
                    start++;
                }

                while (end % sampleLength != 0)
                {
                    max = comparator(max, array[end]);
                    end--;
                }

                for (int i = start / sampleLength; i < end / sampleLength; i++)
                {
                    max = comparator(max, maxes[i]);
                }
            }

            return max;
        }

        public static void A() //Memory limit
        {
            var nmk = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var n = int.Parse(nmk[0]);
            var m = int.Parse(nmk[1]);
            var k = int.Parse(nmk[2]);
            long[,] prefixes = new long[m, n];

            for (int j = 0; j < n; j++)
            {
                var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries); ;

                for (int i = 0; i < m; i++)
                {
                    long prefix = 0;

                    if (j > 0)
                    {
                        prefix += prefixes[i, j - 1];
                    }

                    if (i > 0)
                    {
                        prefix += prefixes[i - 1, j];

                        if (j > 0)
                        {
                            prefix -= prefixes[i - 1, j - 1];
                        }
                    }

                    prefix += int.Parse(input[i]);
                    prefixes[i, j] = prefix;
                }
            }

            for (int i = 0; i < k; i++)
            {
                var x1x2y1y2 = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var y1 = int.Parse(x1x2y1y2[0]) - 1;
                var x1 = int.Parse(x1x2y1y2[1]) - 1;
                var y2 = int.Parse(x1x2y1y2[2]) - 1;
                var x2 = int.Parse(x1x2y1y2[3]) - 1;

                var o = prefixes[x2, y2];

                if (y1 > 0)
                {
                    o -= prefixes[x2, y1 - 1];
                }
                if (x1 > 0)
                {
                    o -= prefixes[x1 - 1, y2];
                }
                if (x1 > 0 && y1 > 0)
                {
                    o += prefixes[x1 - 1, y1 - 1];
                }

                Console.WriteLine(o);
            }
        }
        public static void B()
        {
            var n = int.Parse(Console.ReadLine());
            var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var m = int.Parse(Console.ReadLine());

            var prefix = new long[input.Length];
            prefix[0] = long.Parse(input[0]);

            for (int index = 1; index < input.Length; index++)
            {
                prefix[index] = prefix[index - 1] + long.Parse(input[index]);
            }

            var o = new long[m];

            for (int i = 0; i < m; i++)
            {
                var fs = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var f = int.Parse(fs[0]) - 1;
                var s = int.Parse(fs[1]) - 1;

                o[i] = prefix[s] - ((f - 1) >= 0 ? (prefix[f - 1]) : 0);
            }

            foreach (var output in o)
            {
                Console.Write(output + " ");
            }
        }
        public static void Main(string[] args)
        {
            var n = Console.ReadLine();
            var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var m = int.Parse(Console.ReadLine());

            var array = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                array[i] = int.Parse(input[i]);
            }

            var sampleLength = (int)Math.Sqrt(array.Length);
            var maxes = FindMinMax(array, sampleLength, Math.Min, int.MaxValue);
            var output = " ";

            for (int i = 0; i < m; i++)
            {
                var query = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var s = int.Parse(query[0]) - 1;
                var f = int.Parse(query[1]) - 1;

                output += FindMinMax(array, maxes, sampleLength, s, f, Math.Min, int.MaxValue) + " ";
            }

            Console.WriteLine(output);
        }
    }
}
