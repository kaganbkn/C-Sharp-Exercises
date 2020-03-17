using System;
using System.Text;

namespace Ref_Out
{
    class Program
    {
        //!!  NOTE : "out" and "ref" are referance types.
        /// <summary>
        /// out : metod içinde kullanılması gerek,initialize etmeye gerek yok
        /// ref : metod içinde kullanılması gerekmez,initialize etmemiz gerek
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int a = 1, b = 2;
            Console.WriteLine($"1-a--> {a}, b--> {b}");
            SumOfNumber(a, b);
            Console.WriteLine($"1-a--> {a}, b--> {b}");

            Console.WriteLine($"2-a--> {a}, b--> {b}");
            SumOfNumber1(ref a, b);
            Console.WriteLine($"2-a--> {a}, b--> {b}");

            Console.WriteLine($"3-a--> {b}, c--> ");
            SumOfNumber2(out int c, b);
            Console.WriteLine($"3-a--> {b}, c--> {c}");

            var textA = "world"; //string is a referance type but we need use ref.
            Person.AddHello(textA);
            Console.WriteLine(textA);

            var obj=new Person();
            obj.Name = "zeee";
            Person.SetName(obj);
            Console.WriteLine(obj.Name);

            StringBuilder test = new StringBuilder();
            test.Append("stable");
            Console.WriteLine(test);
            Person.TestI(test);
            Console.WriteLine(test);

            Console.Read();

        }
        static void SumOfNumber(int value, int value2)
        {
            value += value2;
        }
        static void SumOfNumber1(ref int value,int value2)
        {
            value += value2;
        }
        static void SumOfNumber2(out int value, int value2)
        {
            value = 5;
            value += value2;
        }
    }

    class Person
    {
        public string Name { get; set; }

        public static void SetName(Person obj)
        {
            obj.Name = "ez";
        }
        public static void TestI(StringBuilder test)
        {
            test.Append("changing");
        }

        public static void AddHello(string text)
        {
            text += " hello.";
        }
    }
}
