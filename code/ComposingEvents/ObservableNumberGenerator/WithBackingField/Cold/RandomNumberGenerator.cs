using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace ObservableNumberGenerator.WithBackingField.Cold
{
    public class RandomNumberGenerator : IDisposable
    {
        protected Random _random = null;
        protected int _maxNumbersToGenerate;
        private List<IDisposable> _disposables = new List<IDisposable>();

        private IObservable<int> _numbersObservable = null;

        public IObservable<int> Numbers
        {
            get
            {
                return _numbersObservable;
            }
        }

        public RandomNumberGenerator(int maxNumbersToGenerate,
            int startAfterMilliseconds = 1000, int generateEveryMilliseconds = 1000)
        {
            _maxNumbersToGenerate = maxNumbersToGenerate;
            
            _numbersObservable = Observable.Create<int>(observer =>
            {
                _random = new Random();
                var counter = 0;

                var timer = new Timer(o =>
                {
                    try
                    {
                        if (counter == _maxNumbersToGenerate)
                        {
                            observer.OnCompleted();
                        }

                        var n = GenerateNumber();

                        ++counter;

                        observer.OnNext(n);
                    }
                    catch(Exception ex)
                    {
                        observer.OnError(ex);
                    }
                }
                , null, startAfterMilliseconds, generateEveryMilliseconds);

                var disposable =  Disposable.Create(() =>
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    timer.Dispose();
                });

                _disposables.Add(disposable);

                return disposable;
            });
        }

        protected virtual int GenerateNumber()
        {
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
                _disposables.ForEach(d => d.Dispose());
            }
        }
    }
}
