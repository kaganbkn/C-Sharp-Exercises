using System;

namespace Basics
{
    public class Program
    {

        static string _name;
        static string Name
        {
            get
            {
                return _name ?? "Default";
            }
            set
            {
                _name = value;
            }
        }

        static int getTextLength(string text)
        {
            if (text?.Length > 1)
            {
                return text.Length;
            }

            return 0;
        }
        static int getTextLength1(string text)
        {
            return text?.Length > 1 ? text.Length : 0;
        }
        static string getTextLength2(string text)
        {
            return text ?? "Default";
        }
        static void Main(string[] args)
        {

            Console.WriteLine(Name);
            Name = "Perls";
            Console.WriteLine(Name);
            Name = null;
            Console.WriteLine(Name);

            Console.WriteLine(getTextLength(null));
            Console.WriteLine(getTextLength("Hello"));
            Console.WriteLine(getTextLength1(null));
            Console.WriteLine(getTextLength2(null));


            /////////////


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

    public class Three
    {
        static string _name;

        public static string Name
        {
            get { return _name ?? "Default"; }
            set { _name = value; }
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
