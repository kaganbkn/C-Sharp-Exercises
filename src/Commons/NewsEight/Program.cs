using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace NewsEight
{
    class Program
    {

        static void Main(string[] args)
        {
            //struct readonly
            //interface'lere sonradan casting ile method ekleyebiliyoruz.

            int a, b;
            MultipleReturn(out a, out b);
            Console.WriteLine($"Numbers1 : {a},{b}");
            Console.WriteLine($"Numbers2 : {MultipleReturn2().Item1},{MultipleReturn2().Item2}");
            Console.WriteLine($"Numbers3 : {MultipleReturn3().Item1},{MultipleReturn3().Item2}");
            Console.WriteLine($"Numbers4 : {MultipleReturn4().Result.Item1},{MultipleReturn4().Result.Item2}");
            var (number1, number2) = MultipleReturn3(); //tuple Deconstruction
            var (number11, _) = MultipleReturn5().Result; //2. değeri önemsemedik.
            Console.WriteLine(RockPaperScissors("rock", "paper"));

            //using 'de scope'u kaldırabiliriz.
            //async foreach

            Range pRange = ^2..^0;
            var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (var item in numbers[pRange])// numbers[1..4]) //numbers[^3..^0])
            {
                Console.Write(item + ",");
            }

            //int? intExamp = null;
            string strExample = null;
            //strExample = strExample ?? "example"; //eski kullanım
            strExample ??= "example"; //yeni kullanım

            Console.Read();
        }

        public static void MultipleReturn(out int number1, out int number2) //return multiple value, we cannot use out in async
        {
            number1 = 3;
            number2 = 5;
        }

        public static (int, int) MultipleReturn2()
        {
            return (new Random().Next(10, 20), new Random().Next(10, 20));
        }
        public static Tuple<int, int> MultipleReturn3()
        {
            return new Tuple<int, int>(1, 2);
        }
        public static async Task<Tuple<int, int>> MultipleReturn4() //implementation in async
        {
            /////////// Tuples are readonly!!!!
            /// var asd = new Tuple<int, int>(3, 5);
            /// asd.Item2 = 412; //error
            return new Tuple<int, int>(3, 5);
        }
        public static async Task<(int, int)> MultipleReturn5()
        {
            return (3, 5);
        }
        public static string RockPaperScissors(string first, string second)
            => (first, second) switch //converted to tuple
            {
                ("rock", "paper") => "rock is covered by paper. Paper wins.",
                ("rock", "scissors") => "rock breaks scissors. Rock wins.",
                ("paper", "rock") => "paper covers rock. Paper wins.",
                ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
                ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
                ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"  //default yerine underscore kullanılabilir. 
            };

    }
}
