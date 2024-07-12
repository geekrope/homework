//https://acmp.ru/index.asp?main=task&id_task=852

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

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
    public static void Main(string[] args)
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
}