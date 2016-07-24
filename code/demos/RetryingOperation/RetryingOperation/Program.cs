using System;
using System.Net;
using System.Reactive.Linq;
using System.Threading;

namespace RetryingOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            RetryDownloadString();

            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
        }

        static void RetryDownloadString()
        {
            try
            {
                Console.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");

                string url = "http://www.nonexistent.com";

                var observable = Observable.Return<Func<string, string>>(DownloadString)
                    .Select(func => func(url));

                observable.Retry(3).Subscribe(Console.WriteLine, Error, Completed);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Catch reports: {ex.Message}");
            }
        }

        static void Completed()
        {
            Console.WriteLine("The operation completed successfully.");
        }

        static void Error(Exception ex)
        {
            Console.WriteLine($"Error handler reports: There was an error: {ex.Message}");
        }

        static string DownloadString(string url)
        {
            try
            {
                Console.WriteLine($"DownloadString running on thread: {Thread.CurrentThread.ManagedThreadId}");

                return new WebClient().DownloadString(url);
            }
            catch
            {
                Console.WriteLine("Inside DownloadString: An exception occurred.");
                throw;
            }
        }
    }
}
