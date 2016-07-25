using System;

namespace DerivingObservable
{
    interface IEnumerable<T>
    {
        IEnumerator<T> GetEnumerator();
    }

    interface IEnumerator<T>
    {
        // Get the next value
        T MoveNext();

        // Error
        Exception MoveNext();


        // Completion
        void MoveNext();

        void Dispose();
    }
}