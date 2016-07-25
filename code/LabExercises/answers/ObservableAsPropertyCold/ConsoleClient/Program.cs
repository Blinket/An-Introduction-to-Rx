using System;
using System.Reactive.Linq;
using asPropertyCold = ObservableNumberGenerator.ObservableAsProperty.Cold;
using System.Threading;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void TestHotnessObservableAsPropertyCold()
        {
            var random = new asPropertyCold::RandomNumberGenerator(10, 0, 500);
            var observable = random.Numbers;

            observable.Subscribe(n => Console.WriteLine($"Subscription 1: {n}"));
            Thread.Sleep(1000);
            observable.Subscribe(n => Console.WriteLine($"Subscription 2: {n}"));
        }

        static void ObservableAsPropertyCold()
        {
            using (var even = new asPropertyCold::EvenNumberGenerator(10))
            {
                using (var odd = new asPropertyCold::OddNumberGenerator(10))
                {
                    var evenObservable = even.Numbers;

                    var oddObservable = odd.Numbers;

                    int count = 0;

                    var composition = evenObservable.Zip(oddObservable, (n1, n2) => n1 * n2);

                    var subscription = composition.Subscribe(v =>
                    {
                        Console.WriteLine($"{++count} => {v}");
                    }, ex => Console.WriteLine(ex.Message), () => Console.WriteLine($"Completed observation. Values observed: {count}"));

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
        }
    }
}