using System;
using System.Reactive;

namespace FourStepsImplementIObservable
{
    class Program
    {
        static void Main(string[] args)
        {
            DoesNotReportErrorExitsUngracefully();

            Console.WriteLine("Press any key to exit the program...");
            Console.ReadKey();
        }

        static void ViolatesContract()
        {
            var observable = new MyContractViolatingRangeObservable(5, 8);

            var observer = new MyObserver<int>();

            var subscription = observable.Subscribe(observer);

            Console.WriteLine("Press any key to dispose the subscription.");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void ObserverBaseMitigatesContractViolation1()
        {
            var observable = new MyContractViolatingRangeObservable(5, 8);

            var subscription = observable.Subscribe(
                Console.WriteLine,
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Completed."));

            Console.WriteLine("Press any key to dispose the subscription.");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void ObserverBaseMitigatesContractViolation2()
        {
            var observable = new MyContractViolatingRangeObservable(5, 8);

            var observer = Observer.Create<int>(Console.WriteLine,
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Completed."));

            var subscription = observable.Subscribe(observer);

            Console.WriteLine("Press any key to dispose the subscription.");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void Blocking()
        {
            var observable = new MyBlockingRangeObservable(5, 8);

            var subscription1 = observable.Subscribe(new MyObserver<int>());
            subscription1.Dispose();

            Console.WriteLine();

            var subscription2 = observable.Subscribe(new MyObserver<int>("Subscriber 2"));
            subscription2.Dispose();
        }

        static void DemoRaceCondition()
        {
            var observable = new MyThreadUnsafeRangeObservable(5, 8);

            var subscription1 = observable.Subscribe(new MySlowObserver<int>());
            var subscription2 = observable.Subscribe(new MyObserver<int>("Subscriber 2"));

            Console.WriteLine("Press any key to dispose the two subscriptions.");
            Console.ReadKey();
            subscription1.Dispose();
            subscription2.Dispose();
        }

        static void DemoDisposeNeverCalledIfClientForgets()
        {
            var observable = new MyBlockingRangeObservable(5, 8);

            var subscription1 = observable.Subscribe(new MySlowObserver<int>());
        }

        static void DoesNotReportErrorExitsUngracefully()
        {
            var observable = new MyUngracefullyExitingObservable(5, 8);

            var subscription1 = observable.Subscribe(new MyObserver<int>());
            subscription1.Dispose();
        }
    }
}
