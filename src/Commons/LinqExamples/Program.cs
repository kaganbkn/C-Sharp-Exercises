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
            var array11 = new int[] { 6, 7, 9 };
            var array1 = new string[] { "Hello", "from", "win ", "console." };
            var array111 = new int[] { 1, 1, 2, 3, 3, 3, 3, 6, 5, 5, 5, 5 };

            // "Aggregate" applies a method to each element.
            Console.WriteLine("Aggregate : "+array.Aggregate((a, b) => a * b));
            Console.WriteLine("Aggregate : " + array.Aggregate((a, b) => a + b));

            // "All" It tells us if all the elements in a collection match a certain condition.
            Console.WriteLine("All : " + array.All(x => x < 10));
            Console.WriteLine("All : " + array.All(x => x < 3));

            // "Any" It determines if a matching element exists in a collection.
            Console.WriteLine("Any : " + array.All(x => x < 10));
            Console.WriteLine("Any : " + array.All(x => x > 10));

            // "Average"
            Console.WriteLine("Average : " + array.Average());
            Console.WriteLine("Average : " + array1.Average(x=>x.Length));

            // "Concat"
            Console.WriteLine("Concat : ");
            foreach (var item in array.Concat(array11))
            {
                Console.Write(item + ",");
            }

            // "Count"
            Console.WriteLine("\nCount : ");
            Console.WriteLine(array.Count(x => x % 2 == 0));
            var response = from element in array orderby element descending select element;            
            foreach (var item in response)
            {
                Console.Write(item+",");
            }

            // "Distinc"
            Console.WriteLine("\n");
            Console.WriteLine("Distinc : ");
            foreach (var item in array111.Distinct())
            {
                Console.Write(item + ",");
            }

            // "Except"
            Console.WriteLine("\n");
            Console.WriteLine("Except : ");
            foreach (var item in array.Except(array111)) //Remove all array111 from array.
            {
                Console.Write(item + ",");
            }

            // "First"
            Console.WriteLine("\n");
            Console.WriteLine("First : "+array.First(x => x % 2 == 0));
            Console.WriteLine("FirstOrDefault : " + array.FirstOrDefault(x => x % 9 == 0));

            // "Group by"
            Console.WriteLine("Group by : ");
            foreach (var item in array111.GroupBy(a=>a))
            {
                Console.Write(item.Key+",");
            }

            // "Intersect"
            Console.WriteLine("\nIntersect : ");
            foreach (var item in array.Intersect(array111))
            {
                Console.Write(item+",");
            }

            Console.WriteLine("\nLast : "+array.Last());
            Console.WriteLine("Max : " + array.Max());

            // "Skip" "Take" same usage
            Console.WriteLine("\nSkipWhile : ");
            foreach (var item in array.SkipWhile(a=>a<3))
            {
                Console.Write(item+",");
            }

            Console.WriteLine("\nSkip : ");
            foreach (var item in array.Skip(3))
            {
                Console.Write(item + ",");
            }

            // "Select"
            Console.WriteLine("\nSelect : ");
            var temp = array1.Where(a => a == "from").Select(a => a.ToUpper()).ToArray();
            var temp1 = array1.Where(a => a == "from").SelectMany(a => a.ToUpper()).ToArray();
            Console.WriteLine("Select : "+temp[0]);
            Console.WriteLine("SelectMany : "+temp1[0]);

            // "Lookup" //the lookup is based on the key length.
            Console.WriteLine("\nLookup : ");
            var lookup = array1.ToLookup(a => a.Length);
            foreach (var item in lookup[4])
            {
                Console.Write(item+",");
            }

            // "Zip" 
            Console.WriteLine("\nZip : ");
            foreach (var item in array.Zip(array111,(a,b)=>a+b))
            {
                Console.Write(item + ",");
            }

            // "Union" 
            Console.WriteLine("\nUnion : ");
            foreach (var item in array.Union(array111))
            {
                Console.Write(item + ",");
            }

            Console.Read();
        }
    }
}
