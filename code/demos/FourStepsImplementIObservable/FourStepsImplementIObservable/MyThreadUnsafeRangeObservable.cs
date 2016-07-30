using System;
using System.Threading.Tasks;

namespace FourStepsImplementIObservable
{
    class MyThreadUnsafeRangeObservable : IObservable<int>
    {
        private int _start;
        private int _count;
        private int _current;

        public MyThreadUnsafeRangeObservable(int start, int count)
        {
            _start = start;
            _count = count;

            _current = start;
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            try
            {
                Task.Run(() =>
                {
                    for (_current = _start; _current < _start + _count; _current++)
                    {
                        observer.OnNext(_current);
                    }

                    observer.OnCompleted();
                });
                
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
