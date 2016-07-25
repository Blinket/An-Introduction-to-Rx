using System;
using System.Reactive.Linq;
using asPropertyHot = ObservableNumberGenerator.ObservableAsProperty.Hot;
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

        static void TestHotnessObservableAsPropertyHot()
        {
            var random = new asPropertyHot::RandomNumberGenerator(10, 0, 500);
            var observable = random.Numbers;

            observable.Subscribe(n => Console.WriteLine($"Subscription 1: {n}"));
            Thread.Sleep(1000);
            observable.Subscribe(n => Console.WriteLine($"Subscription 2: {n}"));
        }

        static void ObservableAsPropertyHot()
        {
            using (var even = new asPropertyHot::EvenNumberGenerator(10))
            {
                using (var odd = new asPropertyHot::OddNumberGenerator(10))
                {
                    var evenObservable = even.Numbers;

                    var oddObservable = odd.Numbers;

                    int count = 0;

                    var composition = evenObservable.Zip(oddObservable, (n1, n2) => n1 * n2);

                    var subscription = composition.Subscribe(v =>
                    {
                        Console.WriteLine($"{++count} => {v}");
                    });

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
        }
    }
}