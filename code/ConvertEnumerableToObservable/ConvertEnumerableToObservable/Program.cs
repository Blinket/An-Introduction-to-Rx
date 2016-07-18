using System;
using System.Reactive.Linq;

namespace ConvertEnumerableToObservable
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = new[] { "Apple", "Orange", "Grapefruit" };

            var o = names.ToObservable();

            o.Subscribe(Console.WriteLine, () => Console.WriteLine("\nDone"));

            Console.ReadKey();
        }
    }
}
