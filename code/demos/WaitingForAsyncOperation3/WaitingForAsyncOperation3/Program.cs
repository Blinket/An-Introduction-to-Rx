using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reactive.Linq;

namespace WaitingForAsyncOperation3
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Sathyaish\temp\myfriends.txt";

            IEnumerable<Func<string, byte[]>> work = new Func<string, byte[]>[] { ReadFile };

            var observable = from workItem in work.ToObservable()
                             select workItem(path);

            var subscription = observable.Subscribe(Display, Completed);

            Console.WriteLine("Press any key to exit the program.");
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
            var buffer = new byte[256];
            int numBytesRead = 0;

            using (var stream = new FileStream(path, FileMode.Open))
            {
                numBytesRead = stream.Read(buffer, 0, buffer.Length);
            }

            return buffer;
        }
    }
}
