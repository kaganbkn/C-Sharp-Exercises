using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableExample
{
    class Program
    {
        static void Display(IEnumerable<int> array)  //We can pass them directly to methods that receive IEnumerable arguments.
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
        public static IEnumerable<string> Days()
        {
            yield return "Monday";
            yield return "Tuesday";
            yield return "Wednesday";
            yield return "Thursday";
            yield return "Friday";
            yield return "Saturday";
            yield return "Sunday";
        }
        static void Main(string[] args)
        {
            //IEnumerable<int> numbers = from value in Enumerable.Range(0, 10) select value;
            IEnumerable<int> numbers = Enumerable.Range(5, 3); //5,6,7
            var numbers1 = Enumerable.Repeat(1,10);
            numbers1 = Enumerable.Empty<int>();
            foreach (var number in numbers)
            {
                Console.Write(number+",");
            }
            Console.WriteLine("\n Average is : "+numbers.Average());
            List<int> array = numbers.ToList();
            int[] array1 = numbers.ToArray();

            Display(numbers);

            foreach (var day in Days())
            {
                Console.WriteLine(day);
            }
            
            Console.Read();
        }
    }
}
