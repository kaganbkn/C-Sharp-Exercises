using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SemaphoreExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Semaphore semaphoreObject = new Semaphore(initialCount: 3, maximumCount: 3, name: "PrinterApp");

            for (int i = 0; i < 20; ++i)
            {
                int j = i;
                Task.Factory.StartNew(() =>
                {
                    semaphoreObject.WaitOne();
                    Print(j);
                    semaphoreObject.Release();
                });
            }
            Console.WriteLine("ooooooo");

            string var1 = null;
            string var2 = "asd";
            Console.WriteLine(var1 ?? (var2?.Length == 3 ? "yes" : "no"));



            Console.ReadLine();
        }
        public static void Print(int documentToPrint)
        {
            Console.WriteLine("Printing document: " + documentToPrint);
            //code to print document
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
    }
}
