using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Reactive.Linq;

namespace ObservableNumberGenerator.ObservableAsProperty.Hot
{
    public class RandomNumberGenerator : IDisposable
    {
        protected Timer _timer = null;
        protected Random _random = null;
        protected int _counter;
        protected int _maxNumbersToGenerate;

        private Subject<int> _numberSubject = new Subject<int>();

        public IObservable<int> Numbers
        {
            get
            {
                return _numberSubject.AsObservable();
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

                    _numberSubject?.OnCompleted();
                }

                var n = GenerateNumber();

                _numberSubject?.OnNext(n);
            }
            catch(Exception ex)
            {
                _numberSubject?.OnError(ex);
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
