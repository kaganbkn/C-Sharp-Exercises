using System;
using System.Linq;

namespace LinqExamples
{
    class Program
    {
        ///LinQ
        static void Main(string[] args)
        {
            var array = new int[] { 1, 2, 3, 4, 5 };

            // "Aggregate" applies a method to each element.
            Console.WriteLine(array.Aggregate((a, b) => a * b));
            Console.WriteLine(array.Aggregate((a, b) => a + b));

            // "All" It tells us if all the elements in a collection match a certain condition.
            Console.WriteLine(array.All(x => x < 10));
            Console.WriteLine(array.All(x => x < 3));

            // "Any" It determines if a matching element exists in a collection.
            Console.WriteLine(array.All(x => x < 10));
            Console.WriteLine(array.All(x => x > 10));



            Console.Read();
        }
    }
}
