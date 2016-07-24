using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;

namespace ObservableNumberGenerator.UpFromScratch
{
    public class RandomNumbers : IObservable<int>
    {
        protected Random _random = null;
        protected int _maxNumbersToGenerate;
        protected int _startAfterMilliseconds = 1000;
        protected int _generateEveryMilliseconds = 1000;

        private List<ObserverContext<int>> _observers = new List<ObserverContext<int>>();

        public RandomNumbers(int maxNumbersToGenerate,
            int startAfterMilliseconds = 1000, int generateEveryMilliseconds = 1000)
        {
            _maxNumbersToGenerate = maxNumbersToGenerate;
            _startAfterMilliseconds = startAfterMilliseconds;
            _generateEveryMilliseconds = generateEveryMilliseconds;

            _random = new Random();
        }

        protected virtual int GenerateNumber()
        {
            return _random.Next();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");

            var observerContext = new ObserverContext<int>(observer);
            observerContext.Counter = 0;

            var timer = new Timer(ctx =>
            {
                var context = ctx as ObserverContext<int>;

                if (context == null)
                {
                    var msg = "Unable to start timer delegate. Supplied ObserverContext<int> is either null or is not of the correct type.";

                    var ex = new ArgumentNullException("context", msg);

                    observer.OnError(ex);

                    throw ex;
                }

                try
                {
                    if (context.Counter == _maxNumbersToGenerate)
                    {
                        observer.OnCompleted();
                    }

                    var n = GenerateNumber();

                    ++context.Counter;

                    observer.OnNext(n);
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            } , observerContext, _startAfterMilliseconds, _generateEveryMilliseconds);

            observerContext.Timer = timer;

            var subscription = Disposable.Create(() =>
            {
                observerContext?.Timer?.Change(Timeout.Infinite, Timeout.Infinite);
                observerContext?.Timer?.Dispose();
            });

            observerContext.Subscription = subscription;

            _observers.Add(observerContext);

            return subscription;
        }

        ~RandomNumbers()
        {
            _observers?.Select(o => o.Subscription).ToList().ForEach(subscription =>
            {
                subscription.Dispose();
            });
        }

        private class ObserverContext<T>
        {
            public ObserverContext(IObserver<T> observer)
            {
                Observer = observer;
            }

            public IObserver<T> Observer { get; private set; }
            public Timer Timer { get; set; }
            public IDisposable Subscription { get; set; }
            public int Counter { get; set; }
        }
    }
}
