using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Basics.Exceptions;

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


        /// <summary>
        /// ideal usage
        private static string _car;

        public static string Car
        {
            get
            {
                _car = "deneme";
                return _car;
            }
        }

        /// </summary>

        static void Main(string[] args)
        {
            var dateTime = DateTime.ParseExact("Pazar, 12 Nisan 2020", "dddd, dd MMMM yyyy", CultureInfo.CreateSpecificCulture("tr"));

            // "Random"

            Random random = new Random();
            Console.WriteLine(random.Next());
            Console.WriteLine(random.Next(10, 20));

            // "TryParse"
            const string number = "123456";
            const string name = "kagan";
            const string date = "12/05/19";
            if (int.TryParse(number, out int numInt))
            {
                Console.WriteLine($"Parsed Number : {numInt}");
            }

            Console.WriteLine($"Parsed Number : {int.Parse(number)}");

            if (DateTime.TryParse(date, out DateTime dateResult))
            {
                Console.WriteLine($"Parsed Date : {dateResult}");
            }

            int.TryParse(name, out int parsedName);

            Console.WriteLine($"Parsed Name : {parsedName}");  // parsedName initialized default value

            ///////
            ///
            /// 

            Console.WriteLine("------------------------------------");
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
            //var cChar = Console.ReadKey().KeyChar;  //get one char

            // Assign Casdonsole.Title property to string returned by ReadLine.
            Console.Title = Console.ReadLine();

            
            try
            {
                Divider();
            }
            catch (DivideByZeroException e)
            {
                throw new Exception(e.Message);
            }
            catch (ValueIsExceedException e)
            {
                throw new Exception(e.Message); 
            }
            catch (Exception e)  //this be last one.
            {
                throw new Exception(e.Source);
            }
            finally
            {
                Console.WriteLine("From Finally");
            }
            


            //Alternative
            //  Lambda : ()=>{}

            HandleException(() =>   
            {
                Divider();
            });

            //HandleException(Divider);  //Alternative of alternative

            Console.Read();
        }

        private static void HandleException(Action action)  // Action --> void
        {
            try
            {
                action.Invoke();
            }
            catch (DivideByZeroException e)
            {
                throw new Exception(e.Message);
            }
            catch (ValueIsExceedException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)  //this be last one.
            {
                throw new Exception(e.Source);
            }
            finally
            {
                Console.WriteLine("From Finally");
            }

        }

        public static void Divider()
        {
            int value = Convert.ToInt32(Console.ReadLine());
            if (value > 29)
            {
                throw new ValueIsExceedException("Value is bigger than 29."); //message'ı yazabilmek için base constructor'a parametre gönderdik.
            }
            var temp = 30 / value;
        }
    }


    public class Three
    {
        static string _name;

        public static string Name
        {
            get { return _name ?? "Defaultt"; }
            set { _name = value; }
        }
    }

    public class One
    {
        public One() { }
        public One(int value)
        {
            Console.WriteLine("This is One : " + value);
        }
        public One(int value, int value2)
        {
            Console.WriteLine($"This is One : {value} + {value2}");
        }

        //public One() : this(-1, -2)
        //{
        //}

    }
    public class Two : One
    {
        public Two(int value) : base(value)  //value passed to base class
        {
            Console.WriteLine("This is Two : " + value);
        }
    }
}
