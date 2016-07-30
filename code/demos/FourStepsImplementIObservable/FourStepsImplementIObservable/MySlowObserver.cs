using System;
using System.Threading;

namespace FourStepsImplementIObservable
{
    class MySlowObserver<T> : MyObserver<T>
    {
        public MySlowObserver(string subscriberName = "Subscriber 1")
            : base(subscriberName)
        {
        }

        public override void OnNext(T value)
        {
            Thread.Sleep(1000);

            base.OnNext(value);
        }
    }
}
