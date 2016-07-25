using SchoolManagementSystem;
using System;
using System.Reactive.Linq;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var school1 = new School("School 1");
            var school2 = new School("School 2");
            var school3 = new School("School 3");

            var observable = school1
                .Merge(school2)
                .Merge(school3);

            var subscription = observable
                .Subscribe(PrintStudentAdmittedMessage, 
                PrintNoMoreStudentsCanBeAdmittedMessage);

            var period = TimeSpan.FromSeconds(1);

            school1.FillWithStudents(1, period);
            school2.FillWithStudents(2, period);
            school3.FillWithStudents(3, period);

            Console.WriteLine("Press any key to stop observing and to exit the program.");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void PrintStudentAdmittedMessage(Student student)
        {
            Console.WriteLine($"Student admitted: {student}");
        }

        static void PrintNoMoreStudentsCanBeAdmittedMessage()
        {
            Console.WriteLine("No more students can be admitted.");
        }
    }
}
