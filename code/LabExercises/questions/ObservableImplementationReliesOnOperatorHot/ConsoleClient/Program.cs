﻿using System;
using System.Reactive.Linq;
using implOnOperatorHot = ObservableNumberGenerator.ObservableImplementationReliesOnOperator.Hot;
using System.Threading;

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

        static void TestHotnessObservableImplementationReliesOnOperatorHot()
        {
            // TODO: Write the code to test for the hotness or
            // lack thereof of the Numbers property observable
            // of the RandomNumbers class.
        }

        static void ObservableImplementationReliesOnOperatorHot()
        {
            // TODO: Create two observables, one that returns
            // odd numbers and another that returns even numbers.
            // You may use the OddNumberGenerator and EvenNumberGenerator
            // classes in the EventSource project.
            // Compose an observable using the two above-mentioned observables
            // such that it has the product of each value returne from
            // one of the observables with a corresponding value returned
            // by the other.

            // So, for e.g. if each of the observable would have returned the following values:
            // Odd observable: 11, 3, 7, 41
            // Even observable: 2, 4, 20, 8
            // Then the composite observable you create must, if subscribed to, return the following
            // values:
            // Composite observable: 22, 12, 140, 328
        }
    }
}