using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using Polymorphism.Abstract;
using Polymorphism.Data;
using Polymorphism.Interfaces;

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
            Console.WriteLine(Sum(1,2)); //overloading
            Console.WriteLine(Sum(1, 2,3));

            ////////
            ///
            
            A obj=new A();
            obj.Test();
            B obj1=new B();
            obj1.Test();

            var person=new PersonManager();
            person.WriteName(new Manager{Name = "kağan"});
            person.Add(new Workers());

            IPerson[] array=new IPerson[2] //tüm person'lara ekleme yaptık  //yeni bir person tipi eklediğimde hiç bir değişiklik yapmadan add çalışacaktır
            {
                new Manager(), 
                new Workers()
            };

            foreach (var values in array)
            {
                values.Add();
            }

            Database dbRun = new Oracle();
            dbRun.Add();
            dbRun.Delete();

            Database dbRun1 = new SqlServer();
            dbRun1.Add();
            dbRun1.Delete();

            // Manager.Deneme(); // private method'a ulaşılamaz.
            Manager.Deneme1();

            // Interface open/close example

            var intExamp=new InterfaceExample(new Manager());
            intExamp.WriteAdd();

            Console.Read();
        }
    }

    public class A
    {
        private int _value;

        public A()
        {

        }
        public A(int value)
        {
            _value = value;
        }
        public virtual void Test() //virtual fonksiyonlar inherit sınıfta override edilebilir. //interface'de virtual kullanmaya gerek yok zaten.
        {
            Console.WriteLine("A.Test");
        }

        public static void writeA()
        {
            Console.WriteLine("A");
        }
    }

    public class B : A
    {
        public B()
        {

        }
        public B(int value) : base(value)  //base sınıfın constructor'e parametre gönderdik.
        {

        }
        public override void Test()
        {
            base.Test();
            Console.WriteLine("B.Test");
        }
    }
}
