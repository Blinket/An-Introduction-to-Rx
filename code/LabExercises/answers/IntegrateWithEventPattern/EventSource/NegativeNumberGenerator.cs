using System.Threading;

namespace EventSource
{
    public class NegativeNumberGenerator : RandomNumberGenerator
    {
        public NegativeNumberGenerator(int maxNumbersToGenerate,
            int startAfterMilliseconds = 1000, int generateEveryMilliseconds = 1000) : 
            base(maxNumbersToGenerate, startAfterMilliseconds, generateEveryMilliseconds) { }

        protected override int GenerateNumber()
        {
            int n = _random.Next(-10, 0);

            return n;
        }
    }
}
