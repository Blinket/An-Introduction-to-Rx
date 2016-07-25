using System;

namespace EventBasedObserverDesignPattern
{
    interface IObservable<T>
    {
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
    }

    class ObservableNumbers : IObservable<int>
    {
        private event Action<int> _action = null;

        public void Subscribe(Action<int> action)
        {
            _action += action;
        }

        public void Unsubscribe(Action<int> action)
        {
            _action -= action;
        }
    }
}