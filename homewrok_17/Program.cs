using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

public class Program
{
    public static bool Validate(string xml)
    {
        Stack<string> openingTags = new Stack<string>();
        var openingTag = true;
        var openedTag = false;
        var tagName = "";

        for (int index = 0; index < xml.Length;)
        {
            var character = xml[index];

            if (character == '<')
            {
                if (index + 1 >= xml.Length)
                {
                    return false;
                }
                if (xml[index + 1] == '/')
                {
                    openingTag = false;
                    index += 2;
                }
                else
                {
                    index++;
                }

                openedTag = true;
            }
            else if (!openedTag && character != '<')
            {
                return false;
            }
            else if (character == '>')
            {
                if (tagName == "")
                {
                    return false;
                }

                if (openingTag)
                {
                    openingTags.Push(tagName);
                }
                else if (!openingTag)
                {
                    if (openingTags.Count == 0)
                    {
                        return false;
                    }
                    else if (tagName != openingTags.Peek())
                    {
                        return false;
                    }
                    else
                    {
                        openingTags.Pop();
                    }
                }

                tagName = "";
                openingTag = true;
                openedTag = false;

                index++;
            }
            else if (char.IsLetter(character))
            {
                tagName += character;
                index++;
            }
            else
            {
                return false;
            }
        }

        return openingTags.Count == 0;
    }
    public static void A() //Wrong answer 16
    {
        var input = Console.ReadLine();
        var possibleCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ<>/".ToLower();
        var flag = true;

        for (int index = 0; index < input.Length && flag; index++)
        {
            foreach (var substitute in possibleCharacters)
            {
                var changed = new StringBuilder(input);
                changed[index] = substitute;

                if (Validate(changed.ToString()))
                {
                    Console.WriteLine(changed.ToString());
                    flag = false;
                    break;
                }
            }
        }
    }
    public static long GCD(long a, long b)
    {
        if (b == 0)
        {
            return a;
        }

        return GCD(b, a % b);
    }
    public static List<long> Factors(long a)
    {
        var list = new List<long>();

        for (long i = 1; i * i <= a; i++)
        {
            if (a % i == 0)
            {
                if (a / i == i)
                {
                    list.Insert(list.Count / 2, i);
                }
                else
                {
                    list.InsertRange(list.Count / 2, new long[] { a / i, i });
                }
            }
        }

        return list;
    }
    public static int FuncB(List<long> factors, long prev, int p, long n, int k, long product, int count)
    {
        if (count == k)
        {
            return 1;
        }

        var total = 0;

        for (var i = p; i < factors.Count; i++)
        {
            if (GCD(prev, factors[i]) == 1 && product * factors[i] <= n)
            {
                total += FuncB(factors, factors[i], i + 1, n, k, product * factors[i], count + 1);
            }
        }

        return total;
    }
    public static void B()
    {
        var nk = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var n = long.Parse(nk[0]);
        var k = int.Parse(nk[1]);
        var factors = Factors(n);
        var count = 0;

        for (int i = 0; i < factors.Count; i++)
        {
            count += FuncB(factors, factors[i], i + 1, n, k, factors[i], 1);
        }

        Console.WriteLine(count);
    }
    public static void Main(string[] args)
    {
        B();
    }
}