using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;

namespace ObservableNumberGenerator.ObservableImplementationFromScratch.Hot
{
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
                        RaiseOnCompleted();
                    }

                    var n = GenerateNumber();

                    ++_counter;

                    RaiseOnNext(n);
                }
                catch (Exception ex)
                {
                    StopAndDisposeTimer();
                    RaiseOnError(ex);
                }
            }, null, _startAfterMilliseconds, _generateEveryMilliseconds);
        }

        protected virtual void StopAndDisposeTimer()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
            _timerDisposed = true;
        }

        protected virtual void RaiseOnCompleted()
        {
            if (_observers.Count == 0) return;

            foreach(var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        protected virtual void RaiseOnError(Exception ex)
        {
            if (_observers.Count == 0) return;

            foreach (var observer in _observers)
            {
                observer.OnError(ex);
            }
        }

        protected virtual void RaiseOnNext(int value)
        {
            if (_observers.Count == 0) return;

            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }

        protected virtual int GenerateNumber()
        {
            return _random.Next();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");

            _observers.Add(observer);

            if (_timerDisposed)
            {
                RaiseOnCompleted();
            }

            return Disposable.Empty;
        }
    }
}
