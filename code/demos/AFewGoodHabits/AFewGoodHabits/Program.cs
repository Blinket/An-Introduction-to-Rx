using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AFewGoodHabits
{
    class Program
    {
        static void Main(string[] args)
        {
            HoweverDisposeNotCalledAutomaticallyForOngoingSequences();

            Console.ReadKey();
        }

        static void DefaultStubForOnError()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnError(new Exception("Oops! That's all we know."));

                    return Disposable.Create(() =>
                    {
                        Console.WriteLine("Dispose called.");
                    });
                });

                // What happens if you don't:
                // b. Provide an onError: the error bubbles up to the top event handler
                var subscription = observable.Subscribe(ValueHandler, CompletionHandler);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"But we do have the remote stack trace: {ex.StackTrace.ToString()}");
                Console.WriteLine("But still, this is kind of shoddy, don't you think?");
                Console.WriteLine();
            }
        }

        static void ButIfYouProvideOnErrorHandlerItAllWorksNicely()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnError(new Exception("Oops! That's all we know."));

                    return Disposable.Create(() =>
                    {
                        Debug.Print("Dispose() called.");
                        Console.WriteLine("Dispose() called.");
                    });
                });

                // What happens if you don't:
                // b. Provide an onError: the error bubbles up to the top event handler
                var subscription = observable.Subscribe(ValueHandler, ExceptionHandler, CompletionHandler);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"But we do have the remote stack trace: {ex.StackTrace.ToString()}");
                Console.WriteLine("But still, this is kind of shoddy, don't you think?");
                Console.WriteLine();
            }
        }

        static void DefaultStubForOnComplete()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnCompleted();

                    return Disposable.Create(() =>
                    {
                        Debug.Print("Dispose() called.");
                        Console.WriteLine("Dispose() called.");
                    });
                });

                // What happens if you don't:
                // c. Provide a completion handler: the default one does nothing, so you'll
                // never know if and when the sequence completed.
                var subscription = observable.Subscribe(ValueHandler, ExceptionHandler);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        static void DefaultStubForNext()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnCompleted();

                    return Disposable.Create(() =>
                    {
                        Debug.Print("Dispose() called.");
                        Console.WriteLine("Dispose() called.");
                    });
                });

                // What happens if you don't:
                // a. Provide an onNext: the default one ignores the values
                // b. You will never know about a value, and you'll never know if the sequence completed.
                var subscription = observable.Subscribe();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"But we do have the remote stack trace: {ex.StackTrace.ToString()}");
                Console.WriteLine("But still, this is kind of shoddy, don't you think?");
                Console.WriteLine();
            }
        }

        static void YouMustProvideOnNextIfYouWantToProvideAnyOneOrBothOfTheOtherTwo()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnCompleted();

                    return Disposable.Create(() =>
                    {
                        Debug.Print("Dispose() called.");
                        Console.WriteLine("Dispose() called.");
                    });
                });

                // What happens if you don't provide an onNext
                // but want to provide an onCompleted and/or
                // onError handler?
                // You can't. If you want to provide any of the other
                // handlers, you *must* provide a non-null onNext implementation
                // or you will get a NullReferenceException as in the case below.
                // That means, the exception handler you provide will not be honored
                // and will not be installed. Instead, the exception will bubble up
                // to the top-most handler.
                var subscription = observable.Subscribe(onNext: null, onError: ExceptionHandler, onCompleted: CompletionHandler);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"But we do have the remote stack trace: {ex.StackTrace.ToString()}");
                Console.WriteLine("But still, this is kind of shoddy, don't you think?");
                Console.WriteLine();
            }
        }

        static void DisposeCalledAutomaticallyForCompletedSequences()
        {
            // 1. Always call Dispose.
            var observable = Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnCompleted();

                return Disposable.Create(() =>
                {
                    var stackFrame = new StackTrace();
                    var callingMethodBase = stackFrame.GetFrame(1)?.GetMethod();
                    var callingTypeName = callingMethodBase?.DeclaringType.FullName;
                    var callingMethodName = callingMethodBase?.Name;

                    var msg = string.Format($"Dispose called by {callingTypeName}.{callingMethodName}.");

                    Debug.Print(msg);
                    Console.WriteLine(msg);
                });
            });

            var subscription = observable.Subscribe(ValueHandler, ExceptionHandler, CompletionHandler);

            // If you do not call Dispose() on the subscription, 
            // the IObservable<T> implementation will automatically dispose
            // the subscription when the sequence completes.
        }

        static void HoweverDisposeNotCalledAutomaticallyForOngoingSequences()
        {
            // 1. Always call Dispose.
            var observable = Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                
                return Disposable.Create(() =>
                {
                    var stackFrame = new StackTrace();
                    var callingMethodBase = stackFrame.GetFrame(1)?.GetMethod();
                    var callingTypeName = callingMethodBase?.DeclaringType.FullName;
                    var callingMethodName = callingMethodBase?.Name;

                    var msg = string.Format($"Dispose called by {callingTypeName}.{callingMethodName}.");

                    Debug.Print(msg);
                    Console.WriteLine(msg);
                });
            });

            var subscription = observable.Subscribe(ValueHandler, ExceptionHandler, CompletionHandler);

            // If you do not call Dispose() on the subscription, 
            // the IObservable<T> implementation will *not* automatically 
            // if the sequence is still ongoing, i.e. it has not signalled
            // completion and neither thrown an error.
            // Therefore, it is always advisable to be a good citizen
            // and call Dispose on your subscriptions.
        }

        static void DisposedCalledAutomaticallyOnError()
        {
            // 1. Always call Dispose.
            var observable = Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnError(new Exception("Oops! That's all we know."));
                
                return Disposable.Create(() =>
                {
                    var stackFrame = new StackTrace();
                    var callingMethodBase = stackFrame.GetFrame(1)?.GetMethod();
                    var callingTypeName = callingMethodBase?.DeclaringType.FullName;
                    var callingMethodName = callingMethodBase?.Name;

                    var msg = string.Format($"Dispose called by {callingTypeName}.{callingMethodName}.");

                    Debug.Print(msg);
                    Console.WriteLine(msg);
                });
            });

            var subscription = observable.Subscribe(ValueHandler, ExceptionHandler, CompletionHandler);

            // If you do not call Dispose() on the subscription, 
            // the IObservable<T> implementation will automatically dispose
            // the subscription when an exception is raised within the sequence.
        }

        static void ValueHandler(int value)
        {
            Console.WriteLine($"Next value: {value}");
        }

        static void ExceptionHandler(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        static void CompletionHandler()
        {
            Console.WriteLine("We're done.");
        }
    }
}
