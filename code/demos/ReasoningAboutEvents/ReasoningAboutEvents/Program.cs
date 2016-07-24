using SchoolManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ReasoningAboutEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            ObserveAdmissionsPeriodically();
        }

        static void ObserveAdmissionsPeriodically()
        {
            var school = new School("School 1");

            var admissionObservable =
                Observable.FromEventPattern<StudentAdmittedEventArgs>(school, "StudentAdmitted");

            var observables = admissionObservable.Buffer(TimeSpan.FromSeconds(5))
                .Select(lst => lst.Select(ep => ep.EventArgs));

            var subscription = observables.Subscribe(StudentAdmittedValueHandler);

            school.FillWithStudents(10);
            school.FillWithStudentsAsync(3, TimeSpan.FromSeconds(10));
            school.FillWithStudentsAsync(2, TimeSpan.FromSeconds(5));

            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void StudentAdmittedValueHandler(IEnumerable<StudentAdmittedEventArgs> args)
        {
            if (args != null)
            {
                var list = args.ToList();

                var count = list.Count;

                if (count > 0)
                {
                    Console.WriteLine($"New batch of {count} students received:");

                    list.ForEach(arg =>
                    {
                        Console.WriteLine($"{arg.Student} joined {arg.School.Name}");
                    });

                    Console.WriteLine();
                }
            }
        }
    }
}
