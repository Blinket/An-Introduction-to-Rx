using System;

namespace DerivingObservable
{
    interface IObservable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }

    interface IObserver<T>
    {
        // We have a value
        void OnNext(T value);

        // We have an error
        void OnError(Exception ex);

        // Completion
        void OnCompleted();
    }
}
