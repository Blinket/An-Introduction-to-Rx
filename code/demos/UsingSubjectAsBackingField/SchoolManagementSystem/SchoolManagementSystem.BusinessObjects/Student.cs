using System;

namespace SchoolManagementSystem
{
    public class Student
    {
        private static int _studentNumber = 0;
        
        public Student(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        
        public static Student CreateRandom()
        {
            var name = string.Format($"Student {++_studentNumber}");

            return new Student(name);
        }

        public override string ToString()
        {
            return string.Format($"{Name}");
        }
    }
}
