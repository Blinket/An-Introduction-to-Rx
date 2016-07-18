using EventSource;
using System;
using System.Reactive.Linq;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
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

                    var composition = evenObservable.Zip(oddObservable, (n1, n2) => n1 * n2)
                        .Where(n => n < 50);

                    composition.Subscribe(Console.WriteLine);

                    //var composition2 = from n1 in evenObservable
                    //                   from n2 in oddObservable
                    //                   let n3 = n1 * n2
                    //                   where n3 < 50
                    //                   select n3;

                    //composition2.Subscribe(Console.WriteLine);

                    Console.ReadKey();
                }
            }
        }
        
    }
}
