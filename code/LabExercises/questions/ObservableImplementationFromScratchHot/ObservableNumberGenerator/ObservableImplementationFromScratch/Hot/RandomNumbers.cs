using System;
using System.Collections.Generic;
using System.Threading;

namespace ObservableNumberGenerator.ObservableImplementationFromScratch.Hot
{
    // Modify the code in this class to make this class
    // a hot observable but without using any of the in-built 
    // operators such as Publish & Connect or RefCount
    public class RandomNumbers : IObservable<int>
    {
        protected Random _random = null;
        protected int _maxNumbersToGenerate;
        protected int _startAfterMilliseconds = 1000;
        protected int _generateEveryMilliseconds = 1000;

        protected int _counter = 0;
        private List<IObserver<int>> _observers = new List<IObserver<int>>();
        protected Timer _timer = null;
        protected bool _timerDisposed = false;

        public RandomNumbers(int maxNumbersToGenerate,
            int startAfterMilliseconds = 1000, int generateEveryMilliseconds = 1000)
        {
            _maxNumbersToGenerate = maxNumbersToGenerate;
            _startAfterMilliseconds = startAfterMilliseconds;
            _generateEveryMilliseconds = generateEveryMilliseconds;

            _random = new Random();

            _timer = new Timer(o =>
            {
                try
                {
                    if (_counter == _maxNumbersToGenerate)
                    {
                        StopAndDisposeTimer();
                    }

                    var n = GenerateNumber();

                    ++_counter;
                }
                catch (Exception ex)
                {
                    StopAndDisposeTimer();
                }
            }, null, _startAfterMilliseconds, _generateEveryMilliseconds);
        }

        protected virtual void StopAndDisposeTimer()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
            _timerDisposed = true;
        }

        protected virtual int GenerateNumber()
        {
            return _random.Next();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");

            _observers.Add(observer);

            return Disposable.Empty;
        }
    }
}
