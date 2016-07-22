using System;
using System.Reactive.Linq;
using evt = EventSource;
using hot = ObservableNumberGenerator.WithBackingField.Hot;
using cold = ObservableNumberGenerator.WithBackingField.Cold;
using hybrid = ObservableNumberGenerator.Hybrid;
using scratch = ObservableNumberGenerator.UpFromScratch;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //ComposeUsingRxEventBasedZipExample();

            //ComposeUsingRxEventBasedCartesianProductExample();

            //ComposeUsingRxHotObservableZipExample();

            //ComposeUsingRxHotObservableCartesianProductExample();

            //ComposeUsingRxColdObservableZipExample();

            //ComposeUsingRxColdObservableCartesianProductExample();

            //ComposeUsingRxHybridObservableZipExample();

            //ComposeUsingRxHybridObservableCartesianProductExample();

            ComposeUsingRxObservableFromScratchZipExample();

            ComposeUsingRxObservableFromScratchCartesianProductExample();
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
                      }, ex => Console.WriteLine(ex.Message),  () => Console.WriteLine($"Completed observation. Values observed: {count}"));

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

        static void ComposeUsingRxHotObservableZipExample()
        {
            using (var even = new hot::EvenNumberGenerator(10))
            {
                using (var odd = new hot::OddNumberGenerator(10))
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

        static void ComposeUsingRxHotObservableCartesianProductExample()
        {
            using (var even = new hot::EvenNumberGenerator(10))
            {
                using (var odd = new hot::OddNumberGenerator(10))
                {
                    var evenObservable = even.Numbers;

                    var oddObservable = odd.Numbers;

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

        static void ComposeUsingRxColdObservableZipExample()
        {
            using (var even = new cold::EvenNumberGenerator(10))
            {
                using (var odd = new cold::OddNumberGenerator(10))
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

        static void ComposeUsingRxColdObservableCartesianProductExample()
        {
            using (var even = new cold::EvenNumberGenerator(10))
            {
                using (var odd = new cold::OddNumberGenerator(10))
                {
                    var evenObservable = even.Numbers;

                    var oddObservable = odd.Numbers;

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

        static void ComposeUsingRxHybridObservableZipExample()
        {
            var even = new hybrid::EvenNumbers(10);
            var odd = new hybrid::OddNumbers(10);
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

        static void ComposeUsingRxHybridObservableCartesianProductExample()
        {
            var even = new hybrid::EvenNumbers(10);
            var odd = new hybrid::OddNumbers(10);
            int count = 0;

            var composition = from n1 in even
                              from n2 in odd
                              select n1 * n2;

            var subscription = composition.Subscribe(v =>
            {
                Console.WriteLine($"{++count} => {v}");
            }, ex => Console.WriteLine(ex.Message), () => Console.WriteLine($"Completed observation. Values observed: {count}"));

            Console.WriteLine("Press any key to end this subscription.");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void ComposeUsingRxObservableFromScratchZipExample()
        {
            var even = new scratch::EvenNumbers(10);
            var odd = new scratch::OddNumbers(10);
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

        static void ComposeUsingRxObservableFromScratchCartesianProductExample()
        {
            var even = new scratch::EvenNumbers(10);
            var odd = new scratch::OddNumbers(10);
            int count = 0;

            var composition = from n1 in even
                              from n2 in odd
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