using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace FourStepsUsingSubject
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();

            Console.WriteLine("Press any key to exit the program...");
            Console.ReadKey();
        }

        static void Demo1()
        {
            // Using a subject as an observer only. Nothing happens because
            // the default _observer is a NopObserver<T>.Instance, which has
            // an empty body for all the 3 functions.
            var subject = new Subject<string>();

            var subscription = new[] { "Hello", "World!" }.ToObservable().Subscribe(subject);

            Console.WriteLine("Press any key to dispose the subscription.");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void Demo2()
        {
            // Using a subject as a broadcast or relay service
            // That is, a mediator that subscribes to many observables
            // and then relays its values to its observers
            var subject = new Subject<string>();

            var s1 = subject.Subscribe(Program.PrintToConsole);
            var s2 = subject.Subscribe(Program.PrintToDebugWindow);
            var s3 = subject.Subscribe(Program.PrintToConsoleSlowly);

            var o1 = new[] { "Hello", "World" }.ToObservable().Concat(Observable.Never<string>());
            var o2 = new[] { "Sunday", "Monday", "Tuesday" }.ToObservable();

            var s4 = o1.Subscribe(subject);
            var s5 = o2.Subscribe(subject);

            Console.WriteLine("\nPress any key to dispose all subscriptions.");
            s1.Dispose();
            s2.Dispose();
            s3.Dispose();
            s4.Dispose();
            s5.Dispose();
        }

        static void PrintToConsole(string message)
        {
            Console.WriteLine(message);
        }

        static void PrintToConsoleSlowly(string message)
        {
            Thread.Sleep(500);
            Console.WriteLine(message + " slowly...");
        }

        static void PrintToDebugWindow(string message)
        {
            Debug.Print(message);
        }
    }
}