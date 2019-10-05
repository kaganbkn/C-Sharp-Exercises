﻿using System;

namespace Basics
{
    public class Program
    {
        static void Main(string[] args)
        {

            var obj = new Two(10);

            //////////////
            ///
            
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

    public class One
    {
        public One() { }
        public One(int value)
        {
            Console.WriteLine("This is One : "+ value);
        }
        public One(int value,int value2)
        {
            Console.WriteLine($"This is One : {value} + {value2}");
        }

        //public One() : this(-1, -2)
        //{
        //}

    }
    public class Two :One
    {
        public Two(int value):base(value)
        {
            Console.WriteLine("This is Two : " + value);
        }
    }
}   