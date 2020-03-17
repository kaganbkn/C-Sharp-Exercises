using System;
using System.Collections.Generic;
using System.Linq;

namespace TestData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class DummyData
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public string ReverseString(string value)
        {
            var temp = value.ToCharArray();
            Array.Reverse(temp);
            return new string(temp);

        }

        public Dictionary<string, int> Some(Dictionary<string, int> array)
        {



            return array;
        }
    }
}
