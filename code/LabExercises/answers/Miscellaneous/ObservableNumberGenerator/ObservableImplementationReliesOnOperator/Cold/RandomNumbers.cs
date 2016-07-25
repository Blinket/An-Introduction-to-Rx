using System;
using System.Reactive.Linq;

namespace ObservableNumberGenerator.ObservableImplementationReliesOnOperator.Cold
{
    public class RandomNumbers : IObservable<int>
    {
        protected Random _random = null;
        protected int _maxNumbersToGenerate;
        protected int _startAfterMilliseconds = 1000;
        protected int _generateEveryMilliseconds = 1000;

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

            var innerObservable = Observable.Timer(TimeSpan.FromMilliseconds(_startAfterMilliseconds), 
                TimeSpan.FromMilliseconds(_generateEveryMilliseconds))
                .Select(v => GenerateNumber())
                .Take(_maxNumbersToGenerate);

            var subscription = innerObservable.Subscribe(observer);

            return subscription;
        }
    }
}
