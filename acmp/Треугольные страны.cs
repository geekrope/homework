//https://acmp.ru/index.asp?main=task&id_task=90

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
 
    public static decimal Dist(Point p1, Point p2)
    {
        return (decimal)Math.Sqrt((double)((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y)));
    }
 
    public static decimal Area(Point p1, Point p2, Point p3)
    {
        return (new Vector(p2.x - p1.x, p2.y - p1.y, 0) * new Vector(p3.x - p1.x, p3.y - p1.y, 0)).magnitude / 2;
    }
 
    public static bool LiesOnSegment(Point p, Point p1, Point p2)
    {
        return Dist(p1, p) + Dist(p, p2) == Dist(p1, p2);
    }
 
    public static bool LiesInTriagle(Point p, Point p1, Point p2, Point p3)
    {
        return Area(p, p1, p2) + Area(p, p2, p3) + Area(p, p1, p3) == Area(p1, p2, p3)&&!LiesOnSegment(p,p1,p2)&& !LiesOnSegment(p, p2, p3)&& !LiesOnSegment(p, p1, p3);
    }
 
    public static void Main(string[] args)
    {
        var input = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var p = new Point(decimal.Parse(input[0]), decimal.Parse(input[1]));
        var n = int.Parse(Console.ReadLine());
 
        List<int> indices = new List<int>();
 
        for (int i = 0; i < n; i++)
        {
            var triangle = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var p1 = new Point(decimal.Parse(triangle[0]), decimal.Parse(triangle[1]));
            var p2 = new Point(decimal.Parse(triangle[2]), decimal.Parse(triangle[3]));
            var p3 = new Point(decimal.Parse(triangle[4]), decimal.Parse(triangle[5]));
 
            if (LiesInTriagle(p, p1, p2, p3))
            {
                indices.Add(i);
            }
        }
 
        Console.WriteLine(indices.Count);
        foreach (var index in indices)
        {
            Console.Write((index + 1) + " ");
        }
    }
}