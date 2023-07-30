using System;
using System.Runtime.CompilerServices;

namespace Homework3
{
    public class Student
    {
        public string FirstName
        {
            get; private set;
        }
        public string SecondName
        {
            get; private set;
        }

        public override string ToString()
        {
            return FirstName + " " + SecondName;
        }

        public Student(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }
    }

    public class School
    {
        private List<Student> students
        {
            get; set;
        }

        public string Name
        {
            get; private set;
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }
        public void ForEach(Action<Student> itterator)
        {
            foreach (var student in students)
            {
                itterator(student);
            }
        }

        public School(string name)
        {
            Name = name;
            students = new List<Student>();
        }
    }

    public class Program
    {
        private static string ReadString()
        {
            var input = Console.ReadLine();

            if (input != null)
            {
                return input;
            }
            else
            {
                return ReadString();
            }
        }
        private static bool ReadBool()
        {
            var input = Console.ReadLine();

            switch (input)
            {
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return ReadBool();
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("введите название школы");
            var schoolName = ReadString();
            var school = new School(schoolName);
            Console.WriteLine($"Школа {school.Name} успешно создана");

            while (true)
            {
                Console.WriteLine($"Хотите посмотреть список всех учеников школы {school.Name}?");
                var viewStudents = ReadBool();

                if (viewStudents)
                {
                    school.ForEach((student) => { Console.WriteLine(student); });
                }

                Console.WriteLine("Хотите добавить нового ученика?");
                var addStudent = ReadBool();

                if (addStudent)
                {
                    Console.WriteLine("Введите имя ученика");
                    var firstName = ReadString();
                    Console.WriteLine("Введите фамилию ученика");
                    var secondName = ReadString();
                    var student = new Student(firstName, secondName);

                    school.AddStudent(student);
                    Console.WriteLine($"Ученик {student.FirstName} успешно добавлен в школу {school.Name}");
                }
            }
        }
    }
}