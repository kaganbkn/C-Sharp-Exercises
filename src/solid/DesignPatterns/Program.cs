using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var singleton = Singleton.GetInstance();
            var singleton1 = Singleton.GetInstance();
            Console.WriteLine(singleton.Id);
            Console.WriteLine(singleton1.Id);

            //////////////////////

            Console.Read();
        }
    }
}
