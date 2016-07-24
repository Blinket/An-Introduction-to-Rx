using System;
using System.Reactive;
using System.Reactive.Linq;

namespace FourStepsCallObserverCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            // STEP 1
            // Create an observable using one of the operators
            // on the System.Reactive.Linq.Observable class
            var observable = Observable.Range(1, 5);

            var observer = Observer.Create<int>(
                // STEP 3: Receive a value from the observable via the OnNext handler
                Console.WriteLine, /* OnNext handler */
                (error) => { Console.WriteLine($"Error: {error.Message}"); },  /* OnError handler */
                () => { Console.WriteLine("Observation complete"); } /* OnCompleted handler */
                );

            // STEP 2
            // Subscribe to the observable, passing it the observer
            // This kickstarts the observation
            var subscription = observable.Subscribe(observer);

            Console.ReadKey();

            // STEP 4:
            // Dispose off the subscription when you are done observing.
            subscription.Dispose();
        }
    }
}
