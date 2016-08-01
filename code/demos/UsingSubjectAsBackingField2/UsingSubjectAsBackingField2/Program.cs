using SchoolManagementSystem;
using System;
using System.Reactive.Linq;

namespace UsingSubjectAsBackingField
{
    class Program
    {
        static void Main(string[] args)
        {
            var maximumNumberOfSeats = 2;

            var school = new School("School 1");

            var subscription = school
                .Students
                .Take(maximumNumberOfSeats)
                .Subscribe(
                Console.WriteLine, 
                () => Console.WriteLine("No more students can be admitted. Seats are full."));

            school.AdmitStudent(Student.CreateRandom());
            school.AdmitStudent(Student.CreateRandom());
            school.AdmitStudent(Student.CreateRandom());

            Console.WriteLine("Press any key to unsubscribe and exit the program...");
            Console.ReadKey();
            subscription.Dispose();
        }
    }
}
