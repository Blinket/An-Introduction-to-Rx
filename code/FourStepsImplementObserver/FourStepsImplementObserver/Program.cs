using System;
using System.Reactive.Linq;

namespace FourStepsImplementObserver
{
    class Program
    {
        static void Main(string[] args)
        {
            // STEP 1
            // Create an observable using one of the operators
            // on the System.Reactive.Linq.Observable class
            var observable = Observable.Range(1, 5);

            // Create an observer. This is not required
            // as we will see in the next demo.
            var observer = new Observer();

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

    class Observer : IObserver<int>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Observation complete");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }

        public void OnNext(int value)
        {
            // STEP 3:
            // Receive a value from the observable
            Console.WriteLine(value);
        }
    }

}
