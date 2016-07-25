using System;
using System.Reactive.Linq;
using evt = EventSource;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // TODO: Call each of the functions in this class
            // to test your code.


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void CreateObservableOfRandomNumbers()
        {
            using (var random = new evt::RandomNumberGenerator(10))
            {
                // TODO: Create the observable from the event
                // NumberGenerated of the RandomNumberGenerator class
                IObservable<int> observable = null;

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
                // TODO: Write the code to test for the hotness
                // of an observable created from the NumberGenerated
                // event of the RandomNumberGenerator class

                Console.WriteLine("Press any key to unsubscribe.");
                Console.ReadKey();
            }
        }

        static void CreateObservableOfOddAndEvenNumbers()
        {
            // TODO: Use an appropriate operator on the IObservable<T>
            // interface to return both odd and even numbers from 
            // two individual observables, the oddObservable and 
            // the evenObservable as and when they occur.
            // In other words, the composite observable should
            // have values from both the observables in the order
            // they occur as depicted by the following diagram:

            // Odd observable in time: 1------11-------------7--3---|
            // Even observable in time:2--8--6---4---|
            // Resulting composite 
            // observable should have: 1--2--8--6--11--4---7---3----|
        }

        static void MergePositiveAndNegativeNumbersAndQueryForNegative()
        {
            // TODO: Using the PositiveNumberGenerator and the
            // NegativeNumberGenerator classes you would have implemented,
            // create a composite observable that has values 
            // from both the positive and the negative
            // observables as they occur, just as described in the
            // CreateObservableOfOddAndEvenNumbers method's TODO
            // exercise, however, with one additional thing.
            // The composite observable you create should then filter
            // the merged observable for only negative values so that
            // when subscribed to, each subscriber receives only
            // the negative values.

            // So, for e.g. if the observables returned the following values:
            // positive observable:             1******4***12********3***9************|
            // negative observable:             ****-7***-1*****-8***********|
            // Then the merged observable, 
            // an intermediary observable, 
            // must return the following
            // values:                          1***-7*4*-1*12***-8**3***9************|
            // But the finally filtered 
            // observable must return only 
            // the negative number as follows:  ****-7***-1*****-8***********|
        }

        static void CreateObservableOfProductOfOddAndEvenNumbers()
        {
            // TODO: Create two observables, one that returns
            // odd numbers and another that returns even numbers.
            // You may use the OddNumberGenerator and EvenNumberGenerator
            // classes in the EventSource project.
            // Compose an observable using the two above-mentioned observables
            // such that it has the product of each value returne from
            // one of the observables with a corresponding value returned
            // by the other.

            // So, for e.g. if each of the observable would have returned the following values:
            // Odd observable: 11, 3, 7, 41
            // Even observable: 2, 4, 20, 8
            // Then the composite observable you create must, if subscribed to, return the following
            // values:
            // Composite observable: 22, 12, 140, 328
        }
    }
}