using System;

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
            // TODO: Implement the Subscribe method so that
            // the operator RandomNumbers is a cold observable
            // that generates random integers as per the arguments
            // received in its constructor
            throw new NotImplementedException();
        }
    }
}
