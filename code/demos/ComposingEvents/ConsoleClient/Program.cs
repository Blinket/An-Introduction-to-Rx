using System;
using System.Reactive.Linq;
using evt = EventSource;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ComposeUsingRxEventBasedZipExample();

            ComposeUsingRxEventBasedCartesianProductExample();
        }

        static void ComposeUsingRxEventBasedZipExample()
        {
            using (var even = new evt::EvenNumberGenerator(10))
            {
                using (var odd = new evt::OddNumberGenerator(10))
                {
                    var evenObservable = Observable.FromEvent<int>(
                        a => even.NumberGenerated += a,
                        r => even.NumberGenerated -= r);

                    var oddObservable = Observable.FromEvent<int>
                        (a => odd.NumberGenerated += a,
                        r => odd.NumberGenerated -= r);

                    int count = 0;

                    var composition = evenObservable.Zip(oddObservable, (n1, n2) => n1 * n2);

                    var subscription = composition.Subscribe(v =>
                      {
                          Console.WriteLine($"{++count} => {v}");
                      }, 
                      ex => Console.WriteLine(ex.Message),  
                      () => Console.WriteLine($"Completed observation. Values observed: {count}"));

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
        }

        static void ComposeUsingRxEventBasedCartesianProductExample()
        {
            using (var even = new evt::EvenNumberGenerator(10))
            {
                using (var odd = new evt::OddNumberGenerator(10))
                {
                    var evenObservable = Observable.FromEvent<int>(
                        a => even.NumberGenerated += a,
                        r => even.NumberGenerated -= r);

                    var oddObservable = Observable.FromEvent<int>
                        (a => odd.NumberGenerated += a,
                        r => odd.NumberGenerated -= r);

                    int count = 0;

                    var composition = from n1 in evenObservable
                                      from n2 in oddObservable
                                      select n1 * n2;

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