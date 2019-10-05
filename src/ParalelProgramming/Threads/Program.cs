using System;
using System.Threading;

namespace Threads
{
    public class Program
    {
        static void TryThreadPool(Object stateInfo)
        {
            Console.WriteLine("Thread pool...");
            Console.WriteLine("Inline IsThreadPoolThread : " + Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Inline IsBackround : "+Thread.CurrentThread.IsBackground);
        }
        static void Main(string[] args)
        {


            ThreadPool.QueueUserWorkItem(TryThreadPool);
            Console.WriteLine("Main Thread...");
            Console.WriteLine("Main IsThreadPoolThread : " + Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Main IsBackround : " + Thread.CurrentThread.IsBackground);

            ///////////////////
            ///


            Thread t = new Thread(() =>
              {
                  Console.WriteLine("Thread 1");
              });
            t.Start();
            Console.WriteLine(t.IsAlive);
            Console.WriteLine(t.IsBackground);
            t.Join(); //main thread 'e bağlan
            Console.WriteLine("EOF");
        }
    }

    ///////////////
    /// Foregraund threadler biterse Backgraund threadlerin bitmesi beklenmeden program sonlanır.
    /// Foregraund threadler bitmeden program sonlanmaz.
    /// Default olarak threadler backgraund oluşur.
}
