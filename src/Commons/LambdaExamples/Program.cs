using System;
using System.Collections.Generic;

namespace LambdaExamples
{
    class Program
    {
        delegate int Calculate(int money);

        static int CalculateHelper(Calculate func,int money)
        {
            return func(money);
        }

        static int WithTax(int money)
        {
            return money + 18;
        }
        static int WithDoubleTax(int money)
        {
            return money + 36;
        }

        delegate int Numbers(int number);
        static void Main(string[] args) //if the return type is delegate we can be used Lambda.
        {
            var list = new List<int>() {2,6,58,9,3,2 };
            Console.WriteLine(list.FindIndex(x => x % 2 == 1)); //first odd element of list

            Func<int, int> func = x => x+1; //Func is initialized delegete type //also we use Action and Predicate
            Func<int,int,int> func1=(x,y)=> { return x + y; };
            Console.WriteLine(func.Invoke(1));
            Console.WriteLine(func1.Invoke(3,6));


            ///////Delegate
            ///

            Numbers number2 = x => x * x;
            Numbers number3 = x => x * x * x;
            Console.WriteLine(number2(10));
            Console.WriteLine(number3(10));
            Console.WriteLine(CalculateHelper(WithDoubleTax, 100));
            Console.WriteLine(CalculateHelper(WithTax, 100));

            Console.Read();
        }
    }
}
