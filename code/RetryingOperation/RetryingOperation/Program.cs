using System;
using System.Net;
using System.Reactive.Linq;

namespace RetryingOperation
{
    class Program
    {
        private static bool flag = false;

        static void Main(string[] args)
        {
            try
            {
                string url = "http://www.nonexistent.com";

                var observable = Observable.Return<Func<string, string>>(DownloadString)
                    .Select(func => func(url));

                observable.Retry(3).Subscribe(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {

                string url = "http://www.google.com";

                var observable = Observable.Return<Func<string, string>>(DownloadStringEx)
                    .Select(func => func(url));

                observable.Retry(3).Subscribe(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadKey();
        }

        static string DownloadString(string url)
        {
            try
            {
                return new WebClient().DownloadString(url);
            }
            catch
            {
                Console.WriteLine("An exception occurred.");
                throw;
            }
        }

        static string DownloadStringEx(string url)
        {
            try
            {
                if (!flag) url = "http://invalidUrl";

                return new WebClient().DownloadString(url).Length.ToString();
            }
            catch
            {
                flag = !flag;
                Console.WriteLine("Ooops!");
                throw;
            }
        }
    }
}
