using System;

namespace Arrays
{
    public class Program
    {
        static void Main(string[] args)
        {

            var Animals = new string[] { "cat", "dog", "penguin" };
            string[] Animals2 = { "crocodile","tiger" };
            var numbers = new int[] { 1, 2, 3, 4 };
            WriteNumber(numbers);



            Console.ReadLine();
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
