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

        static void QueryCartesianProduct()
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

                    // Re-write this query to insert the side-effect
                    // of printing every value of n3, i.e. the product of n1 and n2
                    // before the filter is applied on n3
                    var composition = from n1 in evenObservable
                                      from n2 in oddObservable
                                      let n3 = n1 * n2
                                      where n3 < 50
                                      select n3;

                    var subscription = composition.Subscribe(Console.WriteLine);

                    Console.WriteLine("Press any key to exit the program.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
        }
    }
}
