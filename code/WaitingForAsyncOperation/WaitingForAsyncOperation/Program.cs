using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForAsyncOperation
{
    class Program
    {
        private static byte[] buffer = new byte[256];
        private static string path = @"C:\Sathyaish\temp\myfriends.txt";
        private static WebClient webClient = new WebClient();

        static void Main(string[] args)
        {
            // from APM
            using (var fStream = new FileStream(path, FileMode.Open))
            {
#pragma warning disable CS0618
                var func = Observable.FromAsyncPattern<byte[], int, int, int>(fStream.BeginRead, fStream.EndRead);
#pragma warning restore CS0618

                var observable = func(buffer, 0, buffer.Length);

                Console.WriteLine("APM read:");
                observable.Subscribe(Display);
                Console.WriteLine();
            }
            
            // from EAP
            webClient.DownloadStringAsync(new Uri("file:///C:/Sathyaish/temp/MyFriends.txt"));
            var observable3 = Observable.FromEventPattern<DownloadStringCompletedEventArgs>(webClient, "DownloadStringCompleted")
                .Select(ep => ep.EventArgs.Result);

            observable3.Subscribe(s => { Console.WriteLine($"EAP read: \n{s}\n"); webClient.Dispose(); });

            Thread.Sleep(3000);

            // from Task
            var observable2 = Observable.FromAsync(() => ReadFileAsync(path));
            observable2.Subscribe(u => 
            {
                Console.WriteLine("TAP read: ");
                Display(buffer.Length);
                Console.WriteLine();
            });

            
            Console.ReadKey();
        }

        public static void Display(int numBytesRead)
        {
            var contents = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
            Console.WriteLine(contents);
        }

        public static async Task<int> ReadFileAsync(string path)
        {
            int numBytesRead = 0;

            using (var stream = new FileStream(path, FileMode.Open))
            {
                numBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            }

            return numBytesRead;
        }
    }
}
