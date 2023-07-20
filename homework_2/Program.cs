using System;
using System.Data.Common;

namespace Homework2
{
    public class Date
    {
        private const int monthsCount = 12;
        public int Day, Month, Year;

        public Date()
        {
            this.Day = 1;
            this.Month = 1;
            this.Year = 1;
        }
        public Date(int day, int month, int year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }

        private static bool IsLeapYear(int year)
        {
            return ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0);
        }
        private static int GetDaysCount(int month, int year)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    if (IsLeapYear(year))
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }
                default:
                    throw new ArgumentException("Not a month number");
            }
        }

        public Date GetTomorrow()
        {
            return this + 1;
        }
        public Date GetYesterday()
        {
            return this - 1;
        }
        public override string ToString()
        {
            return $"{Day}.{Month}.{Year}";
        }
        public void Print()
        {
            Console.WriteLine(this);
        }

        public static Date operator +(Date date, int days)
        {
            var day = date.Day;
            var month = date.Month;
            var year = date.Year;

            while (day + days > GetDaysCount(month, year))
            {
                var fitsYear = (month + 1) <= monthsCount;

                days -= GetDaysCount(month, year);

                month = fitsYear ? month + 1 : 1;
                year = fitsYear ? year : year + 1;
            }

            day += days;

            return new Date(day, month, year);
        }
        public static Date operator -(Date date, int days)
        {
            var day = date.Day;
            var month = date.Month;
            var year = date.Year;

            while (day - days < 1)
            {
                var fitsYear = (month - 1) > 0;

                month = fitsYear ? month - 1 : monthsCount;
                year = fitsYear ? year : year - 1;

                days -= GetDaysCount(month, year);
            }

            day -= days;

            return new Date(day, month, year);
        }
        public static int operator -(Date date1, Date date2)
        {
            return 0;
        }
    }
    public static class Program
    {
        public static void Main(string[] args)
        {
            var date = new Date(30, 9, 2023);

            (date + 1).Print();
            date.GetYesterday().Print();
        }
    }
}