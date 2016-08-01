using System;
using System.Reactive.Linq;

namespace UsingSubjectToBuildIObservableImpl
{
    class Program
    {
        static void Main(string[] args)
        {
            var school = new School();
            school
                .Take(2)
                .Subscribe(
                s => Console.WriteLine($"New student {s} admitted."), 
                ex => Console.WriteLine($"An error ocurred: {ex.Message}"), 
                () => Console.WriteLine("No more students can be admitted to the school."));

            school.AdmitStudent(new Student("Sathyaish"));
            // school.AdmitStudent(null);
            school.AdmitStudent(new Student("Foo"));
            school.AdmitStudent(new Student("Bar"));

            Console.ReadKey();
        }
    }
}
