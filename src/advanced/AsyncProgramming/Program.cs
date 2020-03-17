using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = Task.Run(() => { Console.WriteLine("Selam"); });
            Console.WriteLine("Merhaba");

            // We can use TryAdd, TryUpdate, TryRemove, and TryGetValue to do CRUD operations on ConcurrentDictionary.
            ConcurrentDictionary<string, string> dict = new ConcurrentDictionary<string, string>();
            bool firstItem = dict.TryAdd("1", "First");  //returns true
            bool secondItem = dict.TryAdd("2", "Second");  //returns  true

            bool thirdItem = dict.TryAdd("1", "Third"); //returns false; //key must be unique.

            Console.WriteLine("-->" + firstItem + secondItem + thirdItem);

            dict.TryGetValue("1", out string example);
            Console.WriteLine(example);
            dict.TryGetValue("3", out example);
            Console.WriteLine(example);



            //await kullanmazsak hata fonksiyondan buraya taşınmaz!

            //WebReader();  //we should be use await here.


            LoadData();

            /*
            var list = new List<int> { 1, 2, 3, 4, 5 };
            for (var i = 0; i < 10; i++)
            {
                Task.Run(() => { Move(list, 5, new Random().Next(0, 4)); });
            }
            Thread.Sleep(2000);
            foreach (var item in list)
            {
                Console.Write(item);
            }
            */
            Console.ReadLine();
        }

        public static void Move(List<int> list, int index1, int index2)
        {
            Console.WriteLine("-->" + index1 + index2);
            var temp = list[index1];
            list.RemoveAt(index1);
            list.Insert(index2, temp);
        }

        public static async Task WebReader()
        {
            //Task yerine void kullanmak yanlıştır. async void  kullanırsak hata durumlarını yakalayamayız.Sadece eventlerde?
            using var client = new HttpClient();
            var google =  client.GetAsync("https://www.google.com.tr/");
            var yahoo =  client.GetAsync("https://www.yahoo.com/");
            //google.EnsureSuccessStatusCode();
            var content = await google.Result.Content.ReadAsByteArrayAsync();
            var content1 = await yahoo.Result.Content.ReadAsByteArrayAsync();
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(content));
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(content1));
        }

        public static async Task LoadData()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            var resultOfLines = Task.Run(async () =>  //async ve await çıkarılabilir.
            {
                var lines = await File.ReadAllLinesAsync(@"C:\Workspace\film.csv");
                return lines;
            });

            var writeLines = resultOfLines.ContinueWith(t =>
            {
                var lines = t.Result; //result'ı burada kullanmamızda sakınca yok çünkü bir önceki işlemin bittiğini biliyoruz.
                var count = 0;
                foreach (var item in lines.Skip(2))
                {
                    if (++count > 3)
                    {
                        break;
                    }
                    Console.WriteLine(item);
                }
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            //Eğer yukarıdaki task'da bir hata alınırsa ContinueWith yinede devam eder.ContinueWith çalışması için task'ın tamamlanması yeterli.
            //Bunu TaskContinuationOptions ile engelledik. // OnlyOnRanToCompletion
            //Eğer hata alınırsa aşağıdaki kod çalışacak. //OnlyOnFaulted

            resultOfLines.ContinueWith(t =>
            {
                Console.WriteLine(t.Exception.InnerException.Message);
            }, TaskContinuationOptions.OnlyOnFaulted);

            writeLines.ContinueWith(c =>
            {
                stopwatch.Stop();
                Console.WriteLine("time : " + stopwatch.Elapsed.Milliseconds);

            });

            var lisTask = new List<Task>();
            lisTask.Add(resultOfLines);
            lisTask.Add(writeLines);

            Thread.Sleep(2000);

            if (Task.WhenAll(lisTask).IsCompleted)
            {
                Console.WriteLine("Process is succeeded.");
            }


        }

    }
}
