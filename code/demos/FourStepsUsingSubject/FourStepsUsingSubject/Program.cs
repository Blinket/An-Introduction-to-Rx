using System;
using System.Reactive.Subjects;

namespace FourStepsUsingSubject
{
    class Program
    {
        static void Main(string[] args)
        {
            var subject = new Subject<int>();

            var subscription = subject.Subscribe(
                Console.WriteLine, 
                () => Console.WriteLine("Observation complete."));

            subject.OnNext(1);
            subject.OnNext(2);

            subject.OnCompleted();

            subscription.Dispose();

            Console.WriteLine("Press any key to exit the program...");
            Console.ReadKey();
        }
    }
}
