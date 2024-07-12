//https://acmp.ru/index.asp?main=task&id_task=822

using System;
using System.Globalization;

public class Solution
{
    struct Vector
    {
        public decimal x, y, z;
        public decimal magnitude
        {
            get => (decimal)Math.Sqrt((double)(x * x + y * y + z * z));
        }

        public static Vector operator *(Vector left, Vector right)
        {
            decimal x = left.y * right.z - left.z * right.y;
            decimal y = left.x * right.z - left.z * right.x;
            decimal z = left.x * right.y - left.y * right.x;

            return new Vector(x, y, z);
        }

        public Vector(decimal x, decimal y, decimal z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public static decimal Area(decimal x1, decimal y1, decimal x2, decimal y2, decimal x3, decimal y3)
    {
        return (new Vector(x2 - x1, y2 - y1, 0) * new Vector(x3 - x1, y3 - y1, 0)).magnitude / 2;
    }

    public static void Main(string[] args)
    {
        var inp = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine(Area(decimal.Parse(inp[0]), decimal.Parse(inp[1]), decimal.Parse(inp[2]), decimal.Parse(inp[3]), decimal.Parse(inp[4]), decimal.Parse(inp[5])).ToString(new System.Globalization.CultureInfo("en-US")));
    }
}