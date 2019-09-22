using System;

namespace Arrays
{
    public class Program
    {
        static void Main(string[] args)
        {

            var Animals = new string[] { "cat", "dog", "penguin" };
            string[] Animals2 = { "crocodile","tiger" };
            Console.WriteLine(Array.IndexOf(Animals, "dog"));
            Console.WriteLine(Array.IndexOf(Animals, "rabbit")); //if not exist return -1

            Test test = new Test();
            Console.WriteLine(test[0]); //using indexer

            string joined = string.Join("|",Animals);
            Console.WriteLine(joined);
            string[] seperated = joined.Split('|');
            //////

            var numbers = new int[] { 1, 2, 3, 4 };
            WriteNumber(numbers);
            
            //////



            Console.ReadLine();
        }

        public class Test
        {
            readonly string[] cars = new string[1] { "volvo"};

            public string[] Elements
            {
                get { return cars; }
            }
            public string this[int index] //indexer uses this keyword
            {
                get { return cars[index]; }
            }
        }

        public static void WriteNumber(int[] array)
        {
            foreach(var number in array)
            {
                Console.WriteLine(number);
            }
        }
    }
}
