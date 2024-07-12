//https://acmp.ru/index.asp?main=task&id_task=390

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

    public static decimal Distance(Point p1, Point p2)
    {
        return (decimal)Math.Sqrt((double)((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y)));
    }

    public static decimal Height(Point p1, Point p2, Point p3)
    {
        return 2 * Area(p1, p2, p3) / Distance(p2, p3);
    }

    public static void Main(string[] args)
    {
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        var input1 = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var input2 = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var point = new Point(decimal.Parse(input2[0]), decimal.Parse(input2[1]));
        var p1 = new Point(decimal.Parse(input1[0]), decimal.Parse(input1[1]));
        var p2 = new Point(decimal.Parse(input1[2]), decimal.Parse(input1[3]));
        var p3 = new Point(decimal.Parse(input1[4]), decimal.Parse(input1[5]));

        var dist = Math.Min(Math.Min(Height(point, p1, p2), Height(point, p2, p3)), Height(point, p1, p3));

        Console.WriteLine(dist.ToString("#.000000"));
    }
}