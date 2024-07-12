//https://acmp.ru/index.asp?main=task&id_task=149

using System;

namespace Homework1
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string count = Console.ReadLine();
            string input = Console.ReadLine();

            string[] splittedInput = input.Split();
            string output = "";

            for (int index = splittedInput.Length - 1; index >= 0; index--)
            {
                output += splittedInput[index] + " ";
            }

            Console.WriteLine(output);
        }
    }
}