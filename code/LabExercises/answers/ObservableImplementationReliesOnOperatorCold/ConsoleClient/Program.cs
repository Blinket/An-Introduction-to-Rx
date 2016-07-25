using System;
using System.Reactive.Linq;
using implOnOperatorCold = ObservableNumberGenerator.ObservableImplementationReliesOnOperator.Cold;
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

        static void TestHotnessObservableImplementationReliesOnOperatorCold()
        {
            var observable = new implOnOperatorCold::RandomNumbers(10, 0, 500);

            observable.Subscribe(n => Console.WriteLine($"Subscription 1: {n}"));
            Thread.Sleep(1000);
            observable.Subscribe(n => Console.WriteLine($"Subscription 2: {n}"));
        }

        static void ObservableImplementationReliesOnOperatorCold()
        {
            var even = new implOnOperatorCold::EvenNumbers(10);
            var odd = new implOnOperatorCold::OddNumbers(10);
            int count = 0;

            var composition = even.Zip(odd, (n1, n2) => n1 * n2);

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