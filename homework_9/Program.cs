using System;

namespace homework_9
{
    public record class Sequence(int value, int sequence);
    public class Program
    {
        private static void TaskA()
        {
            var count = 2;
            var endWithOne = 1;
            var N = int.Parse(Console.ReadLine());

            for (int n = 2; n <= N; n++)
            {
                //...0 -> ...01, ...00
                //...1 -> ...10

                var endWithOneNew = count - endWithOne;
                count = (count - endWithOne) * 2 + endWithOne;
                endWithOne = endWithOneNew;
            }

            Console.WriteLine(count);
        }
        private static void TaskB()
        {
            var count = 4;
            var endWithOnes = 1;
            var N = int.Parse(Console.ReadLine());

            if (N == 1)
            {
                Console.WriteLine(2);
            }
            else
            {
                for (int n = 3; n <= N; n++)
                {
                    //...11 -> ...110

                    var endWithOneNew = count - endWithOnes;
                    count = (count - endWithOnes) * 2 + endWithOnes;
                    endWithOnes = endWithOneNew;
                }
            }

            Console.WriteLine(count);
        }
        private static void TaskC()
        {
            TaskA();
        }
        private static void TaskD()
        {
            var count = 3;
            var endWithA = 1;
            var N = int.Parse(Console.ReadLine());

            for (int n = 2; n <= N; n++)
            {
                var endWithANew = count - endWithA;
                count = (count - endWithA) * 3 + endWithA * 2;
                endWithA = endWithANew;
            }

            Console.WriteLine(count);
        }
        private static void TaskE()
        {
            throw new NotImplementedException("Solved in class");
        }
        private static void TaskF()
        {
            var array = new int[int.Parse(Console.ReadLine() + 1)];
            for (int i = 2; i < array.Length; i++)
            {
                var min = array[i - 1] + 1;

                if (i % 2 == 0)
                {
                    min = Math.Min(array[i / 2] + 1, min);
                }
                if (i % 3 == 0)
                {
                    min = Math.Min(array[i / 3] + 1, min);
                }

                array[i] = min;
            }

            Console.WriteLine(array[array.Length - 1]);
        }
        public static void Main(string[] args)
        {
            TaskF();
        }
    }
}