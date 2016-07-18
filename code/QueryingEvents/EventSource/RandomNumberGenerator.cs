using System;
using System.Threading;

namespace EventSource
{
    public class RandomNumberGenerator : IDisposable
    {
        protected Timer _timer = null;
        protected Random _random = null;
        protected int _counter;
        protected int _maxNumbersToGenerate;

        public event Action<int> NumberGenerated;

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
            if (_counter == _maxNumbersToGenerate - 1)
                _timer.Change(Timeout.Infinite, Timeout.Infinite);

            var n = GenerateNumber();

            if (NumberGenerated != null)
                NumberGenerated(n);
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
