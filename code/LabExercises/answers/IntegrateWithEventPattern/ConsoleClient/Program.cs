using System;
using System.Reactive.Linq;
using System.Threading;
using evt = EventSource;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void CreateObservableOfRandomNumbers()
        {
            using (var random = new evt::RandomNumberGenerator(10))
            {
                var observable = Observable
                    .FromEvent<int>(
                    addHandler => random.NumberGenerated += addHandler,
                    removeHandler => random.NumberGenerated -= removeHandler);

                var subscription = observable.Subscribe(Console.WriteLine);

                Console.WriteLine("Press any key to unsubscribe.");
                Console.ReadKey();
                subscription.Dispose();
            }
        }

        static void TestHotnessOfEventBasedObservable()
        {
            using (var random = new evt::RandomNumberGenerator(10))
            {
                var observable = Observable
                    .FromEvent<int>(
                    addHandler => random.NumberGenerated += addHandler,
                    removeHandler => random.NumberGenerated -= removeHandler);

                var subscription1 = observable.Subscribe(value => Console.WriteLine($"Subscription 1: {value}"));

                Thread.Sleep(2000);

                var subscription2 = observable.Subscribe(value => Console.WriteLine($"Subscription 2: {value}"));

                Console.WriteLine("Press any key to unsubscribe.");
                Console.ReadKey();

                subscription1.Dispose();
                subscription2.Dispose();
            }
        }

        static void CreateObservableOfOddAndEvenNumbers()
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

                    var composition = oddObservable.Merge(evenObservable);

                    var subscription = composition.Subscribe(Console.WriteLine);

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
        }

        static void MergePositiveAndNegativeNumbersAndQueryForNegative()
        {
            using (var positive = new evt::PositiveNumberGenerator(10))
            {
                using (var negative = new evt::NegativeNumberGenerator(10))
                {
                    var evenObservable = Observable.FromEvent<int>(
                        a => positive.NumberGenerated += a,
                        r => positive.NumberGenerated -= r);

                    var oddObservable = Observable.FromEvent<int>
                        (a => negative.NumberGenerated += a,
                        r => negative.NumberGenerated -= r);

                    var composition = oddObservable
                        .Merge(evenObservable)
                        .Where(n => n < 0);

                    var subscription = composition.Subscribe(Console.WriteLine);

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
        }

        static void CreateObservableOfProductOfOddAndEvenNumbers()
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

                    var composition = evenObservable.Zip(oddObservable, (n1, n2) => n1 * n2);

                    var subscription = composition.Subscribe(Console.WriteLine);

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
        }
    }
}