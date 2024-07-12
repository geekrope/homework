//https://acmp.ru/index.asp?main=task&id_task=114

using System;
using System.Linq;

namespace homework_11
{
    public class Program
    {
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
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();
            var n = int.Parse(input[0]);
            var k = int.Parse(input[1]);

            Console.WriteLine(DFunc(0, n, k, false));
        }
    }
}