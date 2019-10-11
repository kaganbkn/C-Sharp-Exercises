using System;

namespace Polymorphism
{
    class Program
    {
        static int Sum(int a, int b)
        {
            return a + b;
        }
        static int Sum(int a, int b,int c)
        {
            return a + b + c;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Sum(1,2));
            Console.WriteLine(Sum(1, 2,3));

            ////////
            ///
            
            A obj=new A();
            obj.Test();
            B obj1=new B();
            obj1.Test();


            Console.Read();
        }
    }

    public class A
    {
        public virtual void Test()
        {
            Console.WriteLine("A.Test");
        }
    }

    public class B : A
    {
        public override void Test()
        {
            Console.WriteLine("B.Test");
        }
    }
}
