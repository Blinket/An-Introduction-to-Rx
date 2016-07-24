using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerivingObservable
{
    public class Class1
    {
        private IEnumerable<string> strings;
        private IObservable<string> observable;
    }

    //interface IEnumerable<T>
    //{
    //    IEnumerator<T> GetEnumerator();
    //}

    //interface IEnumerator<T>
    //{
    //    Exception MoveNext();
    //    T MoveNext();

    //    void Dispose();
    //}

    //interface IObservable<T>
    //{
    //    IDisposable Subscribe(IObserver<T> observer);
    //}

    //interface IObserver<T>
    //{
    //    void OnNext(T t);
    //    void OnError(Exception ex);
    //    void OnCompleted();
    //}
}
