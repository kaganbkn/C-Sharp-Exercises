using System;
using System.Text.RegularExpressions;

namespace RegexFull
{
    class Program
    {
        static void Main(string[] args)
        {

            Regex regex = new Regex("Hello");

            Console.WriteLine("Match : "+regex.Match("Hello World!"));

            Console.WriteLine("Ismatch : "+regex.IsMatch("Hello World!"));
            foreach (var item in regex.Matches("Hello World! Hello"))
            {
                Console.WriteLine("Matches : "+item);
            }

            regex = new Regex("!$");
            Console.WriteLine("!$ : " + regex.IsMatch("Hello World!"));

            regex = new Regex("^H");
            Console.WriteLine("^H : " + regex.IsMatch("Hello World!"));

            regex = new Regex("k.re");
            foreach (var item in regex.Matches("kare,küre,kore,kere"))
            {
                Console.WriteLine("Matches . : " + item);
            }


            Console.Read();
        }
    }
}
