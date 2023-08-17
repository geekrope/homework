using System;
using System.Linq;

namespace Homework4
{
    public class Program
    {
        public static int IndexBinarySearch(int target, int[] numbers, int start)
        {
            if (numbers.Length == 1)
            {
                return start;
            }
            else
            {
                var middleIndex = numbers.Length / 2;
                int copyFrom = -1;
                int copyLength = -1;

                if (numbers[middleIndex] < target)
                {
                    copyFrom = middleIndex;
                    copyLength = numbers.Length - middleIndex;
                }
                else if (numbers[middleIndex] == target)
                {
                    return start + middleIndex;
                }
                else
                {
                    copyFrom = 0;
                    copyLength = middleIndex;
                }

                int[] newNums = new int[copyLength];
                Array.Copy(numbers, copyFrom, newNums, 0, copyLength);

                return IndexBinarySearch(target, newNums, copyFrom) + start;
            }
        }
        public static int BinarySearch(int target, int[] numbers)
        {
            return numbers[IndexBinarySearch(target, numbers, 0)];
        }
        public static (int[], int[]) GetTwoArrays()
        {
            Console.ReadLine(); // n and k;

            var first = Console.ReadLine().Split();
            var second = Console.ReadLine().Split();

            var array1 = new int[first.Length];
            var array2 = new int[second.Length];

            for (int index = 0; index < first.Length; index++)
            {
                array1[index] = int.Parse(first[index]);
            }
            for (int index = 0; index < second.Length; index++)
            {
                array2[index] = int.Parse(second[index]);
            }

            return (array1, array2);
        }

        public static void A()
        {
            var (array1, array2) = GetTwoArrays();

            foreach (var el in array2)
            {
                Console.WriteLine(BinarySearch(el, array1));
            }
        }

        public static void B()
        {
            var n = int.Parse(Console.ReadLine());

            Console.WriteLine(Math.Ceiling(Math.Log(n) / Math.Log(2)));
        }

        public static void C()
        {
            var (array1, array2) = GetTwoArrays();

            foreach (var el in array2)
            {
                Console.WriteLine(BinarySearch(el, array1) == el ? "YES" : "NO");
            }
        }

        public static void Main(string[] args)
        {
            var (array1, array2) = GetTwoArrays();

            foreach (var element in array2)
            {
                var entry = IndexBinarySearch(element, array1, 0);

                var foundFirst = false;
                var foundSecond = false;

                int firstEntry = entry;
                int secondEntry = entry;

                while (!(foundFirst && foundSecond))
                {
                    if ((firstEntry - 1) >= 0 && array1[firstEntry - 1] == element)
                    {
                        firstEntry--;
                    }
                    else
                    {
                        foundFirst = true;
                    }

                    if ((secondEntry + 1) < array1.Length && array1[secondEntry + 1] == element)
                    {
                        secondEntry++;
                    }
                    else
                    {
                        foundSecond = true;
                    }
                }

                Console.WriteLine((firstEntry + 1) + " " + (secondEntry + 1));
            }
        }
    }
}