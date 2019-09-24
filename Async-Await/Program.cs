using System;
using System.Threading.Tasks;

namespace Async_Await
{
    public class Program
    {
        static void Main(string[] args)
        {
            WriteA();
            WriteB();
            Console.ReadLine();
        }
        public static async Task WriteA()
        {
            await Task.Run(() =>
            {
                for(var i = 0; i < 100; i++)
                {
                    Console.WriteLine("A");
                }
            });
        }
        public static void WriteB()
        {            
            for(var i = 0; i < 25; i++)
            {
                Console.WriteLine("B");
            }
        }
    }
}
