using System.Threading;

namespace EventSource
{
    public class PositiveNumberGenerator : RandomNumberGenerator
    {
        public PositiveNumberGenerator(int maxNumbersToGenerate,
            int startAfterMilliseconds = 1000, int generateEveryMilliseconds = 1000) : 
            base(maxNumbersToGenerate, startAfterMilliseconds, generateEveryMilliseconds) { }

        protected override int GenerateNumber()
        {
            int n = _random.Next(0, 10);

            Interlocked.Increment(ref _counter);

            return n;
        }
    }
}
