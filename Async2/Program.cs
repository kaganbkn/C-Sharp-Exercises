using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Async2
{
    public class Program
    {
        static Task<string> GetGoogleResource()
        {
            HttpClient httpClient = new HttpClient();
            var t = Task.Run(() => {
                return httpClient.GetStringAsync("https://www.google.com").Result;  //result yazdığımız için sonucu bekliyor
            });
            return t;   
        }

        static async Task<string> GetGoogleResourceAsync()
        {
            HttpClient httpClient = new HttpClient();
            return await httpClient.GetStringAsync("https://www.google.com");
        }

        static async Task GetGoogleResourceAsync1()
        {
            HttpClient httpClient = new HttpClient();
            var result= await httpClient.GetStringAsync("https://www.google.com");
            Console.WriteLine(result);
        }

        static void Main(string[] args)
        {
            var person = new Amount();
            person.balance = 50;

            var t1 = new Thread(thread1 => person.Withdraw(20));
            var t2 = new Thread(thread2 => person.Withdraw(20));
            var t3 = new Thread(thread3 => person.Withdraw(20));
            t1.Start();
            t2.Start();
            t3.Start();


            /////////////
            ///

            //var t1 = GetGoogleResource().ContinueWith(task => Console.WriteLine(task.Result));
            //GetGoogleResourceAsync1();


            /////////////
            ///

            Task t = Task.Run(() => {
                Console.WriteLine("Hello");
            });
            Thread.Sleep(1000);
            if (t.IsCompleted)
            {
                Console.WriteLine("OK");
            }

            ////////////
            ///

            var x = Console.ReadKey();
        }
    }
    public class Amount
    {
        public decimal balance { get; set; }
        private Object thisLock = new Object();
        public void Withdraw(decimal amount)  //this function is thread safe because we use "lock"
        {
            lock (thisLock)
            {
                if (amount > balance)
                {
                    throw new Exception("Insufficient funds");
                }
                balance -= amount;
                Console.WriteLine("New Balance : " + balance);
            }
        }
    }
}
