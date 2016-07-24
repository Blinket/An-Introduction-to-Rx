using System;
using System.IO;
using System.Reactive.Linq;
using System.Text;

namespace WaitingForAsyncOperation4
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Sathyaish\temp\myfriends.txt";
        
            var observable = Observable.Start(() => ReadFile(path));
            var subscription = observable.Subscribe(Display, Completed);
            
            Console.ReadKey();
            subscription.Dispose();
        }

        public static void Display(byte[] buffer)
        {
            var contents = ASCIIEncoding.ASCII.GetString(buffer);
            Console.WriteLine(contents);
            Console.WriteLine();
        }

        static void Completed()
        {
            Console.WriteLine("File read completed successfully.");
        }

        public static byte[] ReadFile(string path)
        {
            int numBytesRead = 0;
            var buffer = new byte[256];

            using (var stream = new FileStream(path, FileMode.Open))
            {
                numBytesRead = stream.Read(buffer, 0, buffer.Length);
            }

            return buffer;
        }
    }
}
