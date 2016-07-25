using System;
using asPropertyHot = ObservableNumberGenerator.ObservableAsProperty.Hot;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {   
            // TODO: Call each of the functions in this class
            // to test your code.

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void TestHotnessObservableAsPropertyHot()
        {
            // TODO: After implementing the Numbers property 
            // of the RandomNumberGenerator class, 
            // write the code for this method to test
            // for the hotness of the Numbers property of
            // the RandomNumberGenerator class.
        }

        static void ObservableAsPropertyHot()
        {
            using (var even = new asPropertyHot::EvenNumberGenerator(10))
            {
                using (var odd = new asPropertyHot::OddNumberGenerator(10))
                {
                    var evenObservable = even.Numbers;

                    var oddObservable = odd.Numbers;

                    // TODO: Implement the rest of this method
                    // to subscribe to an observable that takes
                    // the product of each value from the two observables, 
                    // namely, the evenObservable and the oddObservable

                    Console.WriteLine("Press any key to end this subscription.");
                    Console.ReadKey();
                }
            }
        }
    }
}