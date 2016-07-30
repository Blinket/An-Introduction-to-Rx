using System;

namespace FourStepsImplementIObservable
{
    class MyBlockingRangeObservable : IObservable<int>
    {
        private int _start;
        private int _count;

        public MyBlockingRangeObservable(int start, int count)
        {
            _start = start;
            _count = count;
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            try
            {
                for (int i = _start; i < _start + _count; i++)
                {
                    observer.OnNext(i);
                }

                observer.OnCompleted();

                return new MyDisposable();
            }
            catch (Exception ex)
            {
                observer.OnError(ex);

                return new MyDisposable();
            }
        }
    }
}
