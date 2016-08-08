using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Linq;

namespace FourStepsUsingSubject
{
    class Program
    {
        static void Main(string[] args)
        {
            
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
            // That is, a mediator that subscribes to an observable
            // and then relays its values to its observers
            var subject = new Subject<string>();

            var s1 = subject.Subscribe(v => Program.PrintToConsole("Sub 1", v));
            var s2 = subject.Subscribe(v => Program.PrintToDebugWindow("Sub 2", v));
            var s3 = subject.Subscribe(v => Program.PrintToConsoleSlowly("Sub 3", v));

            var o1 = new[] { "Hello", "World" }.ToObservable();
            
            var s4 = o1.Subscribe(subject);
            
            Console.WriteLine("\nPress any key to dispose all subscriptions.");
            s1.Dispose();
            s2.Dispose();
            s3.Dispose();
            s4.Dispose();
        }

        static void Demo3()
        {
            // Using a subject as a broadcast or relay service
            // That is, a mediator that subscribes to many observables
            // and then relays its values to its observers
            var subject = new Subject<string>();

            var s1 = subject.Subscribe(v => Program.PrintToConsole("Sub 1", v));
            var s2 = subject.Subscribe(v => Program.PrintToDebugWindow("Sub 2", v));
            var s3 = subject.Subscribe(v => Program.PrintToConsoleSlowly("Sub 3", v));

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

        static void Demo4()
        {
            var subject = new Subject<int>();

            subject.Subscribe(value => PrintToConsole<int>("Sub 1", value),
                e => Console.WriteLine(e), 
                () => Console.WriteLine("Observation completed by first subscriber"));


            subject.Subscribe(n => 
            {
                if (n == 2)
                    throw new Exception("Oops!");
            }, 
            e => Console.WriteLine(e), 
            () => { throw new Exception("Oops!"); });

            subject.Subscribe(value => PrintToConsole<int>("Sub 3", value),
                e => Console.WriteLine(e), 
                () => Console.WriteLine("Observation completed by third subscriber"));
            
            for(int i = 0; i < 10; i++)
                subject.OnNext(i);

            subject.OnCompleted();
        }

        static void Demo5()
        {
            var observable = Observable
                .Generate<int, int>(0, i => i < 10, i => i + 1, i => i);

            observable.Subscribe(value => PrintToConsole<int>("Sub 1", value),
                e => Console.WriteLine(e),
                () => Console.WriteLine("Observation completed by first subscriber"));

            observable.Subscribe(value => 
            {
                PrintToConsole<int>("Sub 2", value);
                if (value == 2) throw new Exception("Oops!");
            },
            e => Console.WriteLine(e),
            () => { throw new Exception("Oops!"); });

            observable.Subscribe(value => PrintToConsole<int>("Sub 3", value),
                e => Console.WriteLine(e),
                () => Console.WriteLine("Observation completed by third subscriber"));
        }

        static void Demo6()
        {
            IObservable<int> observable = new Func<int>[] 
            {
                () => 1,
                () => 2,
                () => { throw new Exception("Oops!"); },
                () => 2
            }
            .ToObservable()
            .Select(f => f());
        
            observable.Subscribe(value => PrintToConsole<int>("Sub 1", value),
                e => Console.WriteLine(e),
                () => Console.WriteLine("Observation completed by first subscriber"));

            observable.Subscribe(value =>
            {
                PrintToConsole<int>("Sub 2", value);

                if (value == 2)
                    throw new Exception("Oops!");
            },
            e => Console.WriteLine(e),
            () => { throw new Exception("Oops!"); });

            observable.Subscribe(value => PrintToConsole<int>("Sub 3", value),
                e => Console.WriteLine(e),
                () => Console.WriteLine("Observation completed by third subscriber"));
        }

        static void Demo7()
        {
            var subject = new Subject<int>();

            subject.Subscribe(value => PrintToConsole<int>("Sub 1", value),
                e => Console.WriteLine(e),
                () => Console.WriteLine("Observation completed by Sub 1"));


            subject.Subscribe(
                n => 
                {
                    try
                    {
                        Console.WriteLine($"Sub 2: {n}");

                        if (n == 2)
                        {
                            throw new Exception("Oops!");
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Subscriber 2 caught an exception: {ex.Message}");
                    }
                },
                e => Console.WriteLine(e),
                () => 
                {
                    try
                    {
                        throw new Exception("Oops!");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"The observation is complete. Subscriber 2 caught the error: {ex.Message}");
                    }
                });

            subject.Subscribe(value => PrintToConsole<int>("Sub 3", value),
                e => Console.WriteLine(e),
                () => Console.WriteLine("Observation completed by Sub 3"));

            for (int i = 0; i < 10; i++)
                subject.OnNext(i);

            subject.OnCompleted();
        }

        static void PrintToConsole<T>(string subscriberName, T value)
        {
            Console.WriteLine($"{subscriberName}: {value.ToString()}");
        }

        static void PrintToConsoleSlowly<T>(string subscriberName, T value)
        {
            Thread.Sleep(500);
            Console.WriteLine($"{subscriberName}: {value.ToString()} slowly...");
        }

        static void PrintToDebugWindow<T>(string subscriberName, T value)
        {
            Debug.Print(string.Format($"{subscriberName}: {value.ToString()}"));
        }
    }
}