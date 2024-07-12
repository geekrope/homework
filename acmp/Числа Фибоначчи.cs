//https://acmp.ru/index.asp?main=task&id_task=147

using System;

namespace Homework1
{
    public static class Program
    {
        public static void Main(string[] args)
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
    }
}