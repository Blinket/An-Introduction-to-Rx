using System;
using System.Net;
using System.Reactive.Linq;
using System.Threading;

namespace RetryingOperation
{
    class Program
    {
        private static bool flag = false;

        static void Main(string[] args)
        {
            RetryDownloadString();

            RetryDownStringEx();

            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
        }

        static void RetryDownloadString()
        {
            try
            {
                // Create an observable that retries the DownloadString method
                // 3 times before it fails. If the method completes successfully
                // before it is tried 3 times, the observable must also signal
                // completion.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Catch reports: {ex.Message}");
            }
        }

        static void RetryDownStringEx()
        {
            try
            {

                // Create an observable that retries the DownloadStringEx method
                // 3 times before it fails. If the method completes successfully
                // before it is tried 3 times, the observable must also signal
                // completion.
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

        static string DownloadStringEx(string url)
        {
            try
            {
                Console.WriteLine($"DownloadStringEx running on thread: {Thread.CurrentThread.ManagedThreadId}");

                if (!flag) url = "http://invalidUrl";

                return new WebClient().DownloadString(url).Length.ToString();
            }
            catch
            {
                flag = !flag;
                Console.WriteLine("Inside DownloadStringEx catch: Ooops!");
                throw;
            }
        }
    }
}
