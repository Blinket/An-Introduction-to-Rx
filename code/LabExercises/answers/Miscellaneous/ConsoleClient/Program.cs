using System;
using System.Reactive.Linq;
using evt = EventSource;
using asPropertyHot = ObservableNumberGenerator.ObservableAsProperty.Hot;
using asPropertyCold = ObservableNumberGenerator.ObservableAsProperty.Cold;
using implOnOperatorHot = ObservableNumberGenerator.ObservableImplementationReliesOnOperator.Hot;
using implOnOperatorCold = ObservableNumberGenerator.ObservableImplementationReliesOnOperator.Cold;
using fromScratchCold = ObservableNumberGenerator.ObservableImplementationFromScratch.Cold;
using fromScratchHot = ObservableNumberGenerator.ObservableImplementationFromScratch.Hot;
using System.Threading;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TestHotnessObservableImplementationReliesOnOperatorHot();

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

        static void TestHotnessObservableImplementationReliesOnOperatorHot()
        {
            var observable = new implOnOperatorHot::RandomNumbers(10, 0, 500);

            observable.Subscribe(n => Console.WriteLine($"Subscription 1: {n}"));
            Thread.Sleep(1000);
            observable.Subscribe(n => Console.WriteLine($"Subscription 2: {n}"));
        }

        static void TestHotnessObservableAsPropertyCold()
        {
            var random = new asPropertyCold::RandomNumberGenerator(10, 0, 500);
            var observable = random.Numbers;

            observable.Subscribe(n => Console.WriteLine($"Subscription 1: {n}"));
            Thread.Sleep(1000);
            observable.Subscribe(n => Console.WriteLine($"Subscription 2: {n}"));
        }

        static void TestHotnessObservableAsPropertyHot()
        {
            var random = new asPropertyHot::RandomNumberGenerator(10, 0, 500);
            var observable = random.Numbers;

            observable.Subscribe(n => Console.WriteLine($"Subscription 1: {n}"));
            Thread.Sleep(1000);
            observable.Subscribe(n => Console.WriteLine($"Subscription 2: {n}"));
        }

        static void TestHotnessObservableFromScratchCold()
        {
            var observable = new fromScratchCold::RandomNumbers(10, 0, 500);
            
            observable.Subscribe(n => Console.WriteLine($"Subscription 1: {n}"));
            Thread.Sleep(1000);
            observable.Subscribe(n => Console.WriteLine($"Subscription 2: {n}"));
        }

        static void TestHotnessObservableFromScratchHot()
        {
            var observable = new fromScratchHot::RandomNumbers(10, 0, 500);

            observable.Subscribe(n => Console.WriteLine($"Subscription 1: {n}"));
            Thread.Sleep(1000);
            observable.Subscribe(n => Console.WriteLine($"Subscription 2: {n}"));
        }

        static void EventBased()
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
                      }, ex => Console.WriteLine(ex.Message),  () => Console.WriteLine($"Completed observation. Values observed: {count}"));

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
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
                    }, ex => Console.WriteLine(ex.Message), () => Console.WriteLine($"Completed observation. Values observed: {count}"));

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                    subscription.Dispose();
                }
            }
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

        static void ObservableImplementationReliesOnOperatorHot()
        {
            var even = new implOnOperatorHot::EvenNumbers(10);
            var odd = new implOnOperatorHot::OddNumbers(10);
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

        static void ObservableImplementationFromScratchCold()
        {
            var even = new fromScratchCold::EvenNumbers(10);
            var odd = new fromScratchCold::OddNumbers(10);
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

        static void ObservableImplementationFromScratchHot()
        {
            var even = new fromScratchHot::EvenNumbers(10);
            var odd = new fromScratchHot::OddNumbers(10);
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