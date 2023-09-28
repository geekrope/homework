using Microsoft.VisualBasic;
using System;

namespace homework_7
{
    class BaseDate
    {
        protected int day;
        protected int month;
        protected int year;

        public BaseDate(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }

        public virtual string GetFormat()
        {
            return $"год:{year}, месяц:{month}, день:{day}";
        }
    }
    class AmericanDate : BaseDate
    {
        public AmericanDate(int year, int month, int day) : base(year, month, day)
        { }

        public override string GetFormat()
        {
            return $"{month}.{day}.{year}";
        }
    }
    class EuropeanDate : BaseDate
    {
        public EuropeanDate(int year, int month, int day) : base(year, month, day)
        { }

        public override string GetFormat()
        {
            return $"{day}.{month}.{year}";
        }
    }

    class Summator
    {
        protected virtual int Transform(int item)
        {
            return item;
        }

        public int Sum(int n)
        {
            var sum = 0;

            for (int i = 1; i <= n; i++)
            {
                sum += Transform(i);
            }

            return sum;
        }
    }
    class PowerSummator : Summator
    {
        private int p;

        protected override int Transform(int item)
        {
            return (int)Math.Pow(item, p);
        }

        public PowerSummator(int p)
        {
            this.p = p;
        }
    }
    class SquareSummator : PowerSummator
    {
        public SquareSummator() : base(2)
        {

        }
    }
    class CubeSummator : PowerSummator
    {
        public CubeSummator() : base(3)
        {

        }
    }

    class Profile
    {
        private string occupation;

        public virtual string GetInfo()
        {
            return "";
        }
        public string Describe()
        {
            return occupation + ", " + GetInfo();
        }

        public Profile(string occupation)
        {
            this.occupation = occupation;
        }
    }
    class Vacancy : Profile
    {
        private int salary;

        public override string GetInfo()
        {
            return $"Предлагаемая зарплата: {salary}";
        }

        public Vacancy(string occupation, int salary) : base(occupation)
        {
            this.salary = salary;
        }
    }
    class Resume : Profile
    {
        private int experience;

        public override string GetInfo()
        {
            return $"Стаж работы: {experience}";
        }

        public Resume(string occupation, int experience) : base(occupation)
        {
            this.experience = experience;
        }
    }
}
