using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Async_Await
{
    public class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 10; i++)
            {
                SumNegative(i);
            }

            ////////////////
            ///

            /*
            while (true)
            {
                // Start computation.
                Example();
                // Handle user input.
                string result = Console.ReadLine();
                Console.WriteLine("You typed: " + result);
            }*/

            ///////////
            ///

            //g();
            
            ///////////
            ///

            //Callmethod();
            
            Console.ReadLine();
        }


        public static async Task SumNegative(int number)
        {
            var result=await Task.Run(() => GetSum(number)).ContinueWith(task => GetNegative(task));
            Console.WriteLine("The result is "+result+" for a given number "+ number);
        }

        static int GetSum(int number)
        {
            var sum = 0;
            for(var i = 0; i <= number; i++)
            {
                sum += i;
            }
            return sum;
        }
        static int GetNegative(Task<int> task)
        {
            return task.Result * -1;
        }
           

        /// <summary>
        /// /////////////
        /// </summary>
        /// 
        static async void Example()
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // This method runs asynchronously.
            int t = await Task.Run(() => Allocate());
            stopWatch.Stop();
            Console.WriteLine("Compute: " + t+" time--> "+stopWatch.ElapsedMilliseconds);
        }

        static int Allocate()
        {
            // Compute total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    string value = i.ToString();
                    size += value.Length;
                }
            }
            return size;
        }



        /// <summary>
        /// /////////////
        /// </summary>
        static async void f()
        {
            await h();
        }

        static async Task g()
        {
            await h();
        }

        static async Task h()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// //////////
        /// </summary>




        /// <summary>
        /// /////////////
        /// </summary>

        public static async void Callmethod()
        {
            Task<int> task = WriteA();
            WriteB();
            int count = await task;
            WriteCount(count);
        }
        public static async Task<int> WriteA()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for(var i = 0; i < 100; i++)
                {
                    Console.WriteLine("A");
                    count++;
                }
            });
            return count;
        }
        public static void WriteB()
        {            
            for(var i = 0; i < 25; i++)
            {
                Console.WriteLine("B");
            }
        }
        public static void WriteCount(int count)
        {
            Console.WriteLine("Total Count -->" + count);
        }

        /// <summary>
        /// /////////////
        /// </summary>
    }
}
