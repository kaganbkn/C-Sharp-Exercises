using System;

namespace Ref_Out
{
    class Program
    {

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
}
