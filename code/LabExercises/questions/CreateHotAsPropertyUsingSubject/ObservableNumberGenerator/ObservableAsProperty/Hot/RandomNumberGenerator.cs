using System;
using System.Threading;

namespace ObservableNumberGenerator.ObservableAsProperty.Hot
{
    // TODO: Modify the code of this class as necessary.
    // Implement the body of the get property
    // accessor for the Numbers property
    // using a Subject<T> as the backing field.
    // This should create a hot observable.
    // Also ensure that the observable propagates
    // onNext, onCompleted and onError events
    // to subscribers.
    public class RandomNumberGenerator : IDisposable
    {
        protected Timer _timer = null;
        protected Random _random = null;
        protected int _counter;
        protected int _maxNumbersToGenerate;

        public IObservable<int> Numbers
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public RandomNumberGenerator(int maxNumbersToGenerate,
            int startAfterMilliseconds = 1000, int generateEveryMilliseconds = 1000)
        {
            _maxNumbersToGenerate = maxNumbersToGenerate;
            Interlocked.Exchange(ref _counter, 0);
            _random = new Random();
            _timer = new Timer(new TimerCallback(OnNumberGenerated), null, startAfterMilliseconds, generateEveryMilliseconds);
        }

        protected void OnNumberGenerated(object o)
        {
            try
            {
                if (_counter == _maxNumbersToGenerate)
                {
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    _timer.Dispose();
                }

                var n = GenerateNumber();
            }
            catch(Exception ex)
            {
            }
        }

        protected virtual int GenerateNumber()
        {
            Interlocked.Increment(ref _counter);

            return _random.Next();
        }


        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Dispose();
            }
        }
    }
}
