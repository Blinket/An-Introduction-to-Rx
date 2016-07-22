using System;
using System.Collections.Generic;

namespace SchoolManagementSystem
{
    public class Student
    {
        private static int _studentNumber = 0;
        private static readonly Random _random = new Random();

        private static readonly List<string> _streets =
            new List<string>
            {
                "Baker St.",
                "Lake Avenue",
                "Columbia St.",
                "Prospect St.",
                "Roberts Road",
                "Cardinal Drive",
                "Mill St.",
                "12th St.",
                "3rd St.",
                "Chappel St."
            };

        private static readonly List<string> _cities = new List<string>
        {
            "Tokyo", "Delhi", "Tel Aviv", "New York", "Paris", "London", "Frankfurt"
        };

        public Student(string name, string street, string city)
        {
            Name = name;
            Street = street;
            City = city;
        }

        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public static Student CreateRandom()
        {
            var name = string.Format($"Student {++_studentNumber}");
            var street = _streets[_random.Next(0, _streets.Count - 1)];
            var city = _cities[_random.Next(0, _cities.Count - 1)];

            return new Student(name, street, city);
        }

        public override string ToString()
        {
            return string.Format($"{Name} ({Street}, {City})");
        }
    }
}
