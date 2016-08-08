using SchoolManagementSystem;
using System;
using System.Reactive.Linq;

namespace UsingSubjectAsBackingField
{
    class Program
    {
        static void Main(string[] args)
        {
            var school = new School("School 1", 2);

            var subscription1 = school.Students.Subscribe(
                v => Console.WriteLine($"1> {v}"), 
                () => Console.WriteLine("1> No more students can be admitted. Seats are full.\n"));

            school.AdmitStudent(Student.CreateRandom());
            school.AdmitStudent(Student.CreateRandom());
            school.AdmitStudent(Student.CreateRandom());

            var subscription2 = school.Students.Subscribe(v => Console.WriteLine($"2> {v}"), 
                () => Console.WriteLine("2> Seats full.\n"));

            Console.WriteLine("\nPress any key to unsubscribe and exit the program...");
            Console.ReadKey();
            subscription1.Dispose();
        }
    }
}
