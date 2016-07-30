using System;

namespace FourStepsImplementIObservable
{
    class MyContractViolatingRangeObservable : IObservable<int>
    {
        private int _start;
        private int _count;

        public MyContractViolatingRangeObservable(int start, int count)
        {
            _start = start;
            _count = count;
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            try
            {
                observer.OnError(new Exception("Oops!"));

                for(int i = _start; i < _start + _count; i++)
                {
                    observer.OnNext(i);
                }

                observer.OnCompleted();

                return new MyDisposable();
            }
            catch(Exception ex)
            {
                observer.OnError(ex);

                return new MyDisposable();
            }
        }
    }
}
