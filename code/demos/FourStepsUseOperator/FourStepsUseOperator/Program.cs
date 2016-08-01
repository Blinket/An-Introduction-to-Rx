using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace FourStepsUseOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            TimerExample();            
        }

        static void TimerExample()
        {
            var period = TimeSpan.FromMilliseconds(500);

            var observable = Observable.Timer(period, period)
                .Skip(1)
                .Take(3);

            var subscription = observable.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Observation completed."));

            Console.WriteLine("Press any key to exit the program...");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void GetEnumerableItemsPeriodically()
        {
            var period = TimeSpan.FromMilliseconds(500);
            var enumerable = new List<string> { "One", "Two", "Three" };

            var observable = Observable.Interval(period)
                .Zip(enumerable.ToObservable(),
                (n, s) => s);

            var subscription = observable.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Observation completed."));

            Console.WriteLine("Press any key to exit the program...");
            Console.ReadKey();
            subscription.Dispose();
        }
    }
}
