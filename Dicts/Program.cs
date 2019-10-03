using System;
using System.Collections.Generic;

namespace Dicts
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, int>();
            dict.Add("a", 98);
            dict.Add("b", 99);
            if (dict.ContainsKey("a")) 
            {
                Console.WriteLine($"This is a : {dict["a"]}");
            }
            if (dict.ContainsValue(99))
            {
                Console.WriteLine($"This is 99 : {dict["a"]}");
            }
            Console.WriteLine(dict.TryGetValue("a",out int test)); //out is must //int olmasının sebebi value int!!
            Console.WriteLine(test);
            Console.WriteLine(dict.TryGetValue("c", out int test1)); //if is containing return value
            Console.WriteLine(test1);



            Console.Read();
        }
    }
}
