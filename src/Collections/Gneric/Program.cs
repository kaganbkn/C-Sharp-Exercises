using System;
using System.Collections.Generic;
using System.Linq;

namespace Gneric
{
    public class Program
    {
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
            dict.Add(2, "Two");
            dict.Add(3, "Three");

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

    public class GenericClassTwo<T> where T: class   //class : let the referance types - struct : let the value types
    {
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
