//https://acmp.ru/index.asp?main=task&id_task=858

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
            get => (decimal)Math.Sqrt((double)(x * x + y * y + z * z));
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

    public static decimal Area(Point p1, Point p2, Point p3)
    {
        return (new Vector(p2.x - p1.x, p2.y - p1.y, 0) * new Vector(p3.x - p1.x, p3.y - p1.y, 0)).magnitude / 2;
    }

    public static Point Intersection(decimal a1, decimal b1, decimal c1, decimal a2, decimal b2, decimal c2)
    {
        var x = (b2 * c1 - b1 * c2) / (a1 * b2 - a2 * b1);
        var y = (a2 * c1 - a1 * c2) / (a2 * b1 - a1 * b2);

        return new Point(x, y);
    }

    public static void Main(string[] args)
    {
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        var input1 = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var input2 = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var input3 = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

        var p1 = Intersection(decimal.Parse(input1[0]), decimal.Parse(input1[1]), decimal.Parse(input1[2]),
            decimal.Parse(input2[0]), decimal.Parse(input2[1]), decimal.Parse(input2[2]));
        var p2 = Intersection(decimal.Parse(input1[0]), decimal.Parse(input1[1]), decimal.Parse(input1[2]),
           decimal.Parse(input3[0]), decimal.Parse(input3[1]), decimal.Parse(input3[2]));
        var p3 = Intersection(decimal.Parse(input2[0]), decimal.Parse(input2[1]), decimal.Parse(input2[2]),
          decimal.Parse(input3[0]), decimal.Parse(input3[1]), decimal.Parse(input3[2]));

        Console.WriteLine(Area(p1, p2, p3).ToString("#.000"));
    }
}