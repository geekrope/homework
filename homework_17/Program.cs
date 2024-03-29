using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using static Program;

public class Program
{
    public enum TokenType
    {
        openBracket, closeBracket, slash, letter, ending, none
    }
    public static TokenType GetTokenType(char character)
    {
        var tokenType = TokenType.none;
        switch (character)
        {
            case '<':
                return TokenType.openBracket;
            case '>':
                return TokenType.closeBracket;
            case '/':
                return TokenType.slash;
            default:
                if (char.IsLetter(character))
                {
                    return TokenType.letter;
                }

                return TokenType.none;
        }

        return tokenType;
    }
    public static bool Validate(string xml)
    {
        Stack<string> openingTags = new Stack<string>();
        var openingTag = true;
        var tagName = "";

        var expectedTokens = new TokenType[] { TokenType.openBracket };

        for (int index = 0; index < xml.Length; index++)
        {
            var character = xml[index];
            var tokenType = GetTokenType(character);

            if (Array.IndexOf(expectedTokens, tokenType) == -1)
            {
                return false;
            }

            switch (tokenType)
            {
                case TokenType.openBracket:
                    expectedTokens = new TokenType[] { TokenType.slash, TokenType.letter };
                    openingTag = true;
                    tagName = "";
                    break;
                case TokenType.closeBracket:
                    expectedTokens = new TokenType[] { TokenType.openBracket, TokenType.ending };
                    if (openingTag)
                    {
                        openingTags.Push(tagName);
                    }
                    else
                    {
                        if (openingTags.Count == 0 || openingTags.Pop() != tagName)
                        {
                            return false;
                        }
                    }
                    break;
                case TokenType.slash:
                    expectedTokens = new TokenType[] { TokenType.letter
};
                    openingTag = false;
                    break;
                case TokenType.letter:
                    expectedTokens = new TokenType[] { TokenType.letter, TokenType.closeBracket };
                    tagName += character;
                    break;
                default:
                    return false;
            }
        }

        if (Array.IndexOf(expectedTokens, TokenType.ending) == -1)
        {
            return false;
        }

        return openingTags.Count == 0;
    }
    public static void A()
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
        //Console.WriteLine(Validate(Console.ReadLine()));
        A();
    }
}