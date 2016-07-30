using EventBasedObserverDesignPattern;
using System;

namespace DerivingObservable
{
    class Program
    {
        static void Main()
        {
            var observable = new ObservableNumbers();

            Action<int> action = n => Console.WriteLine(n);
            observable.Subscribe(action);

            observable.Unsubscribe(action);
        }
    }
}
