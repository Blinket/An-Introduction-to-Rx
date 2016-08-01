using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace FourStepsObservableCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            NonBlockingLazilyEvaluatedOperatorBased();

            Console.WriteLine("Press any key to exit the program...");
            Console.ReadKey();
        }

        static void BlockingEagerlyEvaluated()
        {
            var observable = Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnNext(2);
                observer.OnNext(3);

                observer.OnCompleted();

                return Disposable.Empty;
            });

            var subscription = observable.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));
        }

        static void NonBlockingLazilyEvaluatedTimerBased()
        {
            var observable = Observable.Create<int>(observer =>
            {
                Timer timer = null;
                int counter = 0;

                try
                {
                    timer = new Timer(o =>
                    {
                        if (counter == 3)
                        {
                            observer.OnCompleted();
                        }

                        observer.OnNext(counter);
                        Interlocked.Increment(ref counter);
                    }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(500));

                    return timer;
                }
                catch(Exception ex)
                {
                    observer.OnError(ex);

                    return timer ?? Disposable.Empty;
                }
            });

            var subscription = observable.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));
        }

        static void NonBlockingLazilyEvaluatedOperatorBased()
        {
            var observable = Observable.Create<long>(observer =>
            {
                try
                {
                    var innerObservable = Observable
                    .Timer(
                        TimeSpan.FromMilliseconds(0),
                        TimeSpan.FromMilliseconds(500))
                        .Skip(1)
                        .Take(3);

                    return innerObservable.Subscribe(observer);
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);

                    return Disposable.Empty;
                }
            });

            var subscription = observable.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));
        }
    }
}
