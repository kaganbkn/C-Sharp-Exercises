using System;
using System.Collections.Generic;
using System.Globalization;

namespace KVPair
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<KeyValuePair<string,int>>();
            list.Add(new KeyValuePair<string, int>("one", 1));

            Dictionary<string, int> dict = new Dictionary<string, int>() { { "two", 2 } }; //return type is KV Pair for dictionary

            foreach (var item in dict)
            {
                Console.WriteLine(item.Key + " " + item.Value+" "+item.GetType());
            }

            //////// Index Of
            ///
            var temp = "Hello World!";
            if (temp.IndexOf("Mars") == -1) //if contains
            {
                Console.WriteLine("Not containing...");
            }
            Console.WriteLine(temp.IndexOf("World"));

            Console.Read();
        }
    }
}
