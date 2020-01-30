using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokenExample
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            IEnumerable<int> numbers = Enumerable.Range(0, 100000000);

            Task task = Task.Factory.StartNew(() =>
            {
                for(var i=0;i<numbers.Count();i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Islem {0} satıda iptal edildi.",i);
                        break;
                        //throw new OperationCanceledException(token);
                    }
                    Console.Write('.');
                }
            },token);

            Console.WriteLine("İptal etmek için bir tuşa basınız.");
            
            Console.ReadKey();

            source.Cancel();

            Thread.Sleep(2000);

            Console.WriteLine("Task Status = {0}", task.Status);

            Console.Read();


        }
    }
}
