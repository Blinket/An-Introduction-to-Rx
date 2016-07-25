using System;

namespace ObservableNumberGenerator.ObservableAsProperty.Cold
{
    // TODO: Implement the property Numbers of the
    // RandomNumberGenerator class as a cold observable
    // which, when subscribed to, genreates random numbers
    // as per the parameters specified by the constructor
    // of this class.
    // You may have to write code in more than one place
    // in this class to implement it.
    public class RandomNumberGenerator : IDisposable
    {
        protected Random _random = null;
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
            throw new NotImplementedException();
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
                
            }
        }
    }
}
