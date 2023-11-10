using System;
using System.Linq;
using System.Reflection.Metadata;

namespace homework_9
{
    public record struct Point(int x, int y);
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

        private static void TaskA2()
        {
            var input = Console.ReadLine().Split();
            var n = int.Parse(input[0]);
            var m = int.Parse(input[1]);
            var cells = new int[n + 1, m + 1];

            cells[1, 1] = 1;

            for (int x = 1; x <= n; x++)
            {
                for (int y = 1; y <= m; y++)
                {
                    var count = 0;

                    if (!(x == 1 && y == 1))
                    {
                        if (x - 1 >= 1)
                        {
                            count += cells[x - 1, y];
                        }
                        if (y - 1 >= 1)
                        {
                            count += cells[x, y - 1];
                        }

                        cells[x, y] = count;
                    }
                }
            }

            Console.WriteLine(cells[n, m]);
        }
        private static void TaskB2()
        {
            var lastRow = new List<int>() { 1 };
            var currentRow = new List<int>() { 1 };
            var rows = int.Parse(Console.ReadLine());

            for (int row = 0; row < rows; row++)
            {
                for (int i = 1; i < lastRow.Count; i++)
                {
                    currentRow.Add(lastRow[i] + lastRow[i - 1]);
                }

                currentRow.Add(1);
                Console.WriteLine(String.Join(' ', currentRow));
                lastRow = currentRow;
                currentRow = new List<int>() { 1 };
            }
        }
        private static void TaskC2()
        {
            throw new NotImplementedException("Solved in class");
        }
        private static int GetAllPossibleRoutesE2(Point point, Dictionary<Point, int> board)
        {
            if (board.ContainsKey(point))
            {
                return board[point];
            }
            else
            {
                var count = 0;

                if (point.x - 1 >= 0 && point.y - 2 >= 0)
                {
                    count += GetAllPossibleRoutesE2(new Point(point.x - 1, point.y - 2), board);
                }
                if (point.x - 2 >= 0 && point.y - 1 >= 0)
                {
                    count += GetAllPossibleRoutesE2(new Point(point.x - 2, point.y - 1), board);
                }

                board.Add(point, count);

                return count;
            }
        }
        private static void TaskE2()
        {
            var input = Console.ReadLine().Split();
            var n = int.Parse(input[0]);
            var m = int.Parse(input[1]);

            var board = new Dictionary<Point, int>();
            board.Add(new Point(0, 0), 1);

            Console.WriteLine(GetAllPossibleRoutesE2(new Point(n - 1, m - 1), board));
        }
        public static void Main(string[] args)
        {
            TaskE2();
        }
    }
}