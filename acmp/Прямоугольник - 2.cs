//https://acmp.ru/index.asp?main=task&id_task=182

using System;
using System.Globalization;
using System.Collections.Generic;

public class Solution
{
    public struct Point
    {
        public decimal x, y;

        public static Point operator +(Point left, Vector right)
        {
            return new Point(left.x + right.x, left.y + right.y);
        }

        public Point(decimal x, decimal y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Vector
    {
        public decimal x, y, z;
        public decimal magnitude
        {
            get = > (decimal)Math.Sqrt((double)(x * x + y * y + z * z));
        }

            public decimal ScalorProduct(Vector vector)
        {
            return x * vector.x + y * vector.y + z * vector.z;
        }

        public static Vector operator *(Vector left, Vector right)
        {
            decimal x = left.y * right.z - left.z * right.y;
            decimal y = left.x * right.z - left.z * right.x;
            decimal z = left.x * right.y - left.y * right.x;

            return new Vector(x, y, z);
        }
        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.x + right.x, left.y + right.y, left.z + right.z);
        }

        public Vector(decimal x, decimal y, decimal z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public static Point RectanglePoint(decimal x1, decimal y1, decimal x2, decimal y2, decimal x3, decimal y3)
    {
        var v11 = new Vector(x2 - x1, y2 - y1, 0);
        var v12 = new Vector(x3 - x1, y3 - y1, 0);
        var v21 = new Vector(x1 - x2, y1 - y2, 0);
        var v22 = new Vector(x3 - x2, y3 - y2, 0);
        var v31 = new Vector(x2 - x3, y2 - y3, 0);
        var v32 = new Vector(x1 - x3, y1 - y3, 0);

        if (v11.ScalorProduct(v12) == 0)
        {
            return new Point(x1, y1) + v11 + v12;
        }
        else if (v21.ScalorProduct(v22) == 0)
        {
            return new Point(x2, y2) + v21 + v22;
        }
        else
        {
            return new Point(x3, y3) + v31 + v32;
        }
    }

    public static void Main(string[] args)
    {
        var inp = Console.ReadLine().Split(new string[]{ " " }, StringSplitOptions.RemoveEmptyEntries);
        var point = RectanglePoint(decimal.Parse(inp[0]), decimal.Parse(inp[1]), decimal.Parse(inp[2]), decimal.Parse(inp[3]), decimal.Parse(inp[4]), decimal.Parse(inp[5]));
        var format = new System.Globalization.CultureInfo("en-US");

        Console.WriteLine(point.x.ToString(format) + " " + point.y.ToString(format));
    }
}