using System;

namespace FourStepsImplementIObservable
{
    internal class MyObserver<T> : IObserver<T>
    {
        private string _name = null;

        public MyObserver(string subscriberName = "Subscriber 1")
        {
            _name = subscriberName;
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine($"Observation completed by {_name}.");
        }

        public virtual void OnError(Exception error)
        {
            Console.WriteLine($"An error occurred while {_name} was observing: {error.Message}");
        }

        public virtual void OnNext(T value)
        {
            Console.WriteLine($"{_name} received: {value.ToString()}");
        }
    }
}