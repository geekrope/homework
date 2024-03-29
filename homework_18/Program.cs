using System;
using System.Collections.Generic;

namespace homework_18
{
    interface ISegmentTreeNode
    {
        int Left
        {
            get;
        }
        int Right
        {
            get;
        }
        int Data
        {
            get;
        }

        void Update(int index, int value);
        int? Query(int left, int right);
    }
    public class SegmentTreeNode : ISegmentTreeNode
    {
        private int left, right, data;
        private Func<int, int, int> comparator;
        private ISegmentTreeNode leftChild, rightChild;

        public int Left => left;
        public int Right => right;
        public int Data => data;

        public void Update(int index, int value)
        {
            if (left <= index && right >= index)
            {
                leftChild.Update(index, value);
                rightChild.Update(index, value);

                data = comparator(leftChild.Data, rightChild.Data);
            }
            else
            {
                return;
            }
        }
        public int? Query(int left, int right)
        {
            if (this.right < left || this.left > right)
            {
                return null;
            }
            else if (this.left >= left && this.right <= right)
            {
                return data;
            }
            else
            {
                var query1 = leftChild.Query(left, right);
                var query2 = rightChild.Query(left, right);

                if (query1.HasValue && !query2.HasValue)
                {
                    return query1.Value;
                }
                else if (!query1.HasValue && query2.HasValue)
                {
                    return query2.Value;
                }
                else if (query1.HasValue && query2.HasValue)
                {
                    return comparator(query1.Value, query2.Value);
                }

                return null;
            }
        }

        public SegmentTreeNode(int left, int right, int[] array, Func<int, int, int> comparator)
        {
            this.left = left;
            this.right = right;
            this.comparator = comparator;

            if (right > left)
            {
                var mid = (left + right) / 2;
                leftChild = mid == left ? (ISegmentTreeNode)(new SegmentTreeLeaf(mid, array)) : (ISegmentTreeNode)(new SegmentTreeNode(left, mid, array, comparator));
                rightChild = right == mid + 1 ? (ISegmentTreeNode)(new SegmentTreeLeaf(right, array)) : (ISegmentTreeNode)(new SegmentTreeNode(mid + 1, right, array, comparator));
                data = comparator(leftChild.Data, rightChild.Data);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
    public class SegmentTreeLeaf : ISegmentTreeNode
    {
        private int index, data;

        public int Left => index;
        public int Right => index;
        public int Data => data;

        public void Update(int index, int value)
        {
            if (this.index == index)
            {
                data = value;
            }
        }
        public int? Query(int left, int right)
        {
            if (left <= index && right >= index)
            {
                return data;
            }

            return null;
        }

        public SegmentTreeLeaf(int index, int[] array)
        {
            this.index = index;
            this.data = array[index];
        }
    }
    public class SegmentTree
    {
        private ISegmentTreeNode root;

        public void Update(int index, int value)
        {
            root.Update(index, value);
        }
        public int? Query(int left, int right)
        {
            return root.Query(left, right);
        }

        public SegmentTree(int[] array, Func<int, int, int> comparator)
        {
            if (array.Length == 1)
            {
                root = new SegmentTreeLeaf(0, array);
            }
            else
            {
                root = new SegmentTreeNode(0, array.Length - 1, array, comparator);
            }
        }
    }

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

            if (maxes.Count * sampleLength < array.Length)
            {
                maxes.Add(max);
            }

            return maxes.ToArray();
        }
        public static int FindMinMax(int[] array, int[] maxes, int sampleLength, int start, int end, Func<int, int, int> comparator, int defaultValue)
        {
            var max = defaultValue;

            while (start % sampleLength != 0 && start <= end)
            {
                max = comparator(max, array[start]);
                start++;
            }

            while (end % sampleLength != 0 && start <= end)
            {
                max = comparator(max, array[end]);
                end--;
            }

            if (start < end)
            {
                for (int i = start / sampleLength; i < end / sampleLength; i++)
                {
                    max = comparator(max, maxes[i]);
                }
            }

            return max;
        }

        public static int MaxMinOnSegemnt(int[] array, int start, int end, Func<int, int, int> comparator, int defaultValue)
        {
            var max = defaultValue;

            for (var i = start; i <= end; i++)
            {
                max = comparator(max, array[i]);
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
        public static void B() //Memory limit
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
        public static void C() //Wrong answer
        {
            var n = Console.ReadLine();
            var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var m = int.Parse(Console.ReadLine());

            var array = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                array[i] = int.Parse(input[i]);
            }

            var tree = new SegmentTree(array, Math.Max);

            //var sampleLength = (int)Math.Sqrt(array.Length);
            //var maxes = FindMinMax(array, sampleLength, Math.Max, 0);
            //FindMinMax(array, maxes, sampleLength, s, e, Math.Max, 0)
            var output = " ";

            for (int i = 0; i < m; i++)
            {
                var query = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var s = int.Parse(query[0]) - 1;
                var e = int.Parse(query[1]) - 1;

                output += tree.Query(s, e) + " ";
            }

            Console.WriteLine(output);
        }
        public static void D() //Wrong answer
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
                var e = int.Parse(query[1]) - 1;

                output += FindMinMax(array, maxes, sampleLength, s, e, Math.Min, int.MaxValue) + " ";
            }

            Console.WriteLine(output);
        }

        public static void Main(string[] args)
        {
            C();
        }
    }
}
