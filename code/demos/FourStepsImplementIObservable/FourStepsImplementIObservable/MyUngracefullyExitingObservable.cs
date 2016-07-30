using System;

namespace FourStepsImplementIObservable
{
    class MyUngracefullyExitingObservable : IObservable<int>
    {
        private int _start;
        private int _count;

        public MyUngracefullyExitingObservable(int start, int count)
        {
            _start = start;
            _count = count;
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            for (int i = _start; i < _start + _count; i++)
            {
                if (i % 2 == 0)
                {
                    // If something that is not really apparent
                    // say a side-effect causes this exception
                    throw new Exception("Oops!");
                }

                observer.OnNext(i);
            }

            observer.OnCompleted();

            return new MyDisposable();
        }
    }
}
