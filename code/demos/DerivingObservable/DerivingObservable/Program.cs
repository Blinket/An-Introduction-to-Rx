using EventBasedObserverDesignPattern;
using System;

namespace DerivingObservable
{
    class Program
    {
        static void Main()
        {
            var observable = new ObservableNumbers();

            observable.Subscribe(n => Console.WriteLine(n));

            observable.Unsubscribe(n => Console.WriteLine(n));
        }
    }
}
