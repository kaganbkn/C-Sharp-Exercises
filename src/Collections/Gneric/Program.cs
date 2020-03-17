using System;
using System.Collections.Generic;
using System.Linq;

namespace Gneric
{
    public class Program
    {
        static void Swap<T>(ref T input1, ref T input2) where T :struct 
        {
            var temp = input2;
            input2 = input1;
            input1 = temp;
        }
        static void Swap1<T>(T input1, T input2) where T : class
        {
            //var temp = default(T);

            var temp = input2;
            input2 = input1;
            input1 = temp;
        }

        static int AddSome(int a, out int b)
        {
            b = 5;
            return a + b;
        }

        public static int AddTwo(int a )
        {
            return a + 2;
        }
        public static void AddMore(ref string text)
        { 
            text += " !!!";
        }
        public static void Main(string[] args)
        {
            MyGenericClass<int,string> obj=new MyGenericClass<int, string>(10);
            obj.GenericMethod("words");

            var objTwo = new GenericClassTwo<String>();
            var objThree = new GenericClassThree<int>();
            var objFourty= new GenericArray<int>();
            objFourty.GenericCollectionPrint(Enumerable.Range(3,9));

            objTwo.WriteText("deneme");
            objThree.WriteText(63);

            IDictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "One");

            int first = 4;
            int second = 5;
            string first1 = "a";
            string second1= "b";

            Swap<int>(ref first, ref second); // "out" and "ref" are referance types.
            Swap1<string>(first1, second1);
            Console.WriteLine($"first : {first} , second : {second}");
            Console.WriteLine($"first1 : {first1} , second1 : {second1}");

            var number = 3;
            Console.WriteLine(AddTwo(number));

            Console.WriteLine(AddSome(5, out int OutValue));
            Console.WriteLine(OutValue);
            var deneme123= "merhaba";
            AddMore(ref deneme123);
            Console.WriteLine(deneme123);

            Console.Read();
        }
    }

    public class GenericArray<TKey>
    {
        public void GenericCollectionPrint(IEnumerable<TKey> array)
        {
            foreach (var key in array)
            {
                Console.Write($"{key},");
            }
            Console.WriteLine();
        }
    }

    public class MyGenericClass<TKey,TKey1>
    {
        private TKey genericMember;

        public MyGenericClass(TKey value)
        {
            genericMember = value;
        }
        public TKey GenericMethod(TKey1 genericParameter)
        {
            Console.WriteLine("Parameter type: {0}, value: {1}", typeof(TKey1).ToString(), genericParameter);
            Console.WriteLine("Return type: {0}, value: {1}", typeof(TKey).ToString(), genericMember);

            return genericMember;
        }

        public TKey genericProperty { get; set; }
    }

    public class GenericClassTwo<T> where T: class //, new() --> birden fazla yazılabilir.(new'lenebilir olmasını istiyoruz(string olmaz))   //buraya interface ismi yazılıp kısıt verilebilir.
    {                                               //class : allow the referance types - struct : allow the value types
        public T Text { get; set; }

        public void WriteText(T Text)
        {
            Console.WriteLine("Value : " + Text);
        }
        //Note : Reference Types--> String,Class,Delegate,Arrays (heap)
        //       Value Types--> int,char,byte,long,enum ... (stack)
    }
    public class GenericClassThree<T> where T : struct   
    {
        public T Text { get; set; }

        public void WriteText(T Text)
        {
            Console.WriteLine("Value : " + Text);
        }      
    }
}
