using System;
using System.Collections.Generic;

namespace Dicts
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, int>(); // Key must be unique.
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
            Console.WriteLine(dict.TryGetValue("a",out int test)); //out is must //int olmasının sebebi dicts value int!!
            Console.WriteLine(test);
            Console.WriteLine(dict.TryGetValue("c", out int test1)); //if is containing return value
            Console.WriteLine(test1);
            foreach (var item in dict)
            {
                Console.WriteLine($"Key--> {item.Key} Value--> {item.Value}");
            }
            var list = new List<int>(dict.Values);

            dict["a"] = 100;
            dict.Remove("a");
            Console.WriteLine(dict.Count);
            dict.Clear();
            Console.Read();
        }
    }
}
