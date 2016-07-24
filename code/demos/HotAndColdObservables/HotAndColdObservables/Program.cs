using System;
using System.Reactive.Linq;

namespace HotAndColdObservables
{
    class Program
    {
        static void Main(string[] args)
        {
            UsingRefCountYouMustSubscribeToTheConnectedObservable();

            UsingRefCountSubscribedToTheConnectedObservable();

            Console.WriteLine("Press any key to exit the program.");
            Console.ReadKey();
        }

        static void UsingRefCountYouMustSubscribeToTheConnectedObservable()
        {
            var observable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Do(value => Console.WriteLine($"Publishing value {value}"))
                .Publish();

            Console.WriteLine("\nPress any key to start publishing values.");
            Console.ReadKey();
            var refCountObservable = observable.RefCount();
            Console.WriteLine("\nNo values published because there are no subscribers");

            Console.WriteLine("\nPress any key to start subscription.");
            Console.ReadKey();
            var subscription1 = observable.Subscribe(value => Console.WriteLine($"Subscriber 1 OnNext received value: {value}"));

            Console.WriteLine("\nPress any key to start another subscription.");
            Console.ReadKey();
            var subscription2 = observable.Subscribe(value => Console.WriteLine($"Subscriber 2 OnNext received value: {value}"));
            
            Console.WriteLine("\nPress any key to unsubscribe the first subscriber.");
            Console.ReadKey();
            subscription1.Dispose();

            Console.WriteLine("\nPress any key to unsubscribe the second subscriber.");
            Console.ReadKey();
            subscription2.Dispose();

            //Console.WriteLine("\nPress any key to stop publishing values.");
            //Console.ReadKey();
            //refCountObservable.Dispose();

            Console.WriteLine("\nPress any key to start a third subscription.");
            Console.ReadKey();
            var subscription3 = observable.Subscribe(value => Console.WriteLine($"Subscriber 3 OnNext received value: {value}"));

            Console.WriteLine("\nPress any key to unsubscribe the third subscriber.");
            Console.ReadKey();
            subscription3.Dispose();

            Console.WriteLine("Disconnected from the observable because it has no subscribers");
        }

        static void UsingRefCountSubscribedToTheConnectedObservable()
        {
            var observable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Do(value => Console.WriteLine($"Publishing value {value}"))
                .Publish();

            Console.WriteLine("\nPress any key to start publishing values.");
            Console.ReadKey();
            var refCountObservable = observable.RefCount();
            Console.WriteLine("No values published because there are no subscribers");

            Console.WriteLine("\nPress any key to start subscription.");
            Console.ReadKey();
            var subscription1 = refCountObservable.Subscribe(value => Console.WriteLine($"Subscriber 1 OnNext received value: {value}"));

            Console.WriteLine("\nPress any key to start another subscription.");
            Console.ReadKey();
            var subscription2 = refCountObservable.Subscribe(value => Console.WriteLine($"Subscriber 2 OnNext received value: {value}"));

            Console.WriteLine("\nPress any key to unsubscribe the first subscriber.");
            Console.ReadKey();
            subscription1.Dispose();

            Console.WriteLine("\nPress any key to unsubscribe the second subscriber.");
            Console.ReadKey();
            subscription2.Dispose();

            Console.WriteLine("\nPress any key to stop publishing values.");
            Console.ReadKey();
            Console.WriteLine("\nOops! You cannot control the publication of values on this observable because it maintains a reference count of its subscribers and will stop publishing values automatically when there are no subscribers.");

            Console.WriteLine("\nPress any key to start a third subscription.");
            Console.ReadKey();
            var subscription3 = refCountObservable.Subscribe(value => Console.WriteLine($"Subscriber 3 OnNext received value: {value}"));

            Console.WriteLine("\nPress any key to unsubscribe the third subscriber.");
            Console.ReadKey();
            subscription3.Dispose();

            Console.WriteLine("Disconnected from the observable because it has no subscribers");
        }

        static void UsingPublishAndConnect()
        {
            var observable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Do(value => Console.WriteLine($"Publishing value {value}"))
                .Publish();

            Console.WriteLine("\nPress any key to start publishing values.");
            Console.ReadKey();
            var connectedDisposable = observable.Connect();

            Console.WriteLine("\nPress any key to start subscription.");
            Console.ReadKey();
            var subscription1 = observable.Subscribe(value => Console.WriteLine($"Subscriber 1 OnNext received value: {value}"));

            Console.WriteLine("\nPress any key to start another subscription.");
            Console.ReadKey();
            var subscription2 = observable.Subscribe(value => Console.WriteLine($"Subscriber 2 OnNext received value: {value}"));


            Console.WriteLine("\nPress any key to unsubscribe the first subscriber.");
            Console.ReadKey();
            subscription1.Dispose();

            //Console.WriteLine("\nPress any key to unsubscribe the second subscriber.");
            //Console.ReadKey();
            //subscription2.Dispose();

            Console.WriteLine("\nPress any key to stop publishing values.");
            Console.ReadKey();
            connectedDisposable.Dispose();

            Console.WriteLine("\nPress any key to start a third subscription.");
            Console.ReadKey();
            var subscription3 = observable.Subscribe(value => Console.WriteLine($"Subscriber 3 OnNext received value: {value}"));

            Console.WriteLine("\nPress any key to unsubscribe the third subscriber.");
            Console.ReadKey();
            subscription3.Dispose();

            Console.WriteLine("\nPress any key to unsubscribe the second subscriber.");
            Console.ReadKey();
            subscription2.Dispose();
        }
    }
}
