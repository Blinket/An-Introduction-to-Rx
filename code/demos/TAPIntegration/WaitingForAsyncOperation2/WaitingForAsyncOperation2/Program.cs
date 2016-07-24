using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace WaitingForAsyncOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Sathyaish\temp\myfriends.txt";

            var observable = ReadFileAsync(path).ToObservable();

            observable.Subscribe(Display, () => Console.WriteLine("File read completed successfully."));
            
            Console.ReadKey();
        }

        public static void Display(byte[] buffer)
        {
            var contents = ASCIIEncoding.ASCII.GetString(buffer);
            Console.WriteLine(contents);
            Console.WriteLine();
        }

        public static async Task<byte[]> ReadFileAsync(string path)
        {
            int numBytesRead = 0;
            var buffer = new byte[256];

            using (var stream = new FileStream(path, FileMode.Open))
            {
                numBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            }

            return buffer;
        }
    }
}
