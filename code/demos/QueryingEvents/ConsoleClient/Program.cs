using EventSource;
using System;
using System.Reactive.Linq;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryZip();
        }
        
        static void QueryZip()
        {
            using (var even = new EvenNumberGenerator(10))
            {
                using (var odd = new OddNumberGenerator(10))
                {
                    var evenObservable = Observable.FromEvent<int>(
                        a => even.NumberGenerated += a,
                        r => even.NumberGenerated -= r);

                    var oddObservable = Observable.FromEvent<int>
                        (a => odd.NumberGenerated += a,
                        r => odd.NumberGenerated -= r);

                    var composition = evenObservable
                        .Zip(oddObservable, (n1, n2) => n1 * n2)
                        .Do(product => Console.WriteLine($"Product : {product}"))
                        .Where(product => product < 50);

                    var subscription = composition.Subscribe(Console.WriteLine);

                    Console.WriteLine("Press any key to exit the program.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
        }
    }
}
