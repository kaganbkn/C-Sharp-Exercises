using System;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            //var cChar = Console.ReadKey().KeyChar;

            // Assign Casdonsole.Title property to string returned by ReadLine.
            Console.Title = Console.ReadLine();

            try
            {
                var a = Convert.ToInt32(Console.ReadLine());
                var temp = 30 / a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Source);
            }
            finally
            {
                Console.WriteLine("From Finally");
            }

        }
    }
}   
