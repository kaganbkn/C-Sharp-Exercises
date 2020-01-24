using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    public class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>() { 1, 2, 3 };
            Console.WriteLine(numbers[1]);
            numbers.Add(4);
            var numbers1 = new List<int>();
            numbers1.AddRange(numbers);
            foreach (var number in numbers1)  //numbers.Count
            {
                Console.WriteLine("Number in numbers array --> " + number);
            }
            numbers1.ForEach(a => Console.WriteLine("linq numbers1 -->" + a));
            if (numbers1.TrueForAll(a => a > 0))
            {
                Console.WriteLine("All elements higher than 0.");
            }

            // numbers.SequenceEqual(numbers2)
            // rivers.GetRange(1, 2)
            // list.Reverse()
            // dogs.Insert(1, "dalmatian") //added 1. index

            var books = new List<Books>();
            books.AddRange(new Books[2]
            {
                new Books{Id = 1,Name = "kitap"},
                new Books{Id = 2,Name = "kitap1"}
            });

            foreach (var book in books)
            {
                Console.WriteLine($"Id : {book.Id} , Name : {book.Name}");
            }

            Console.ReadKey();
        }
    }

    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
