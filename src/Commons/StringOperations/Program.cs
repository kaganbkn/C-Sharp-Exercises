using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace StringOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            // "String Builder , Replace"
            string name = "kagan";
            Console.WriteLine(name.Replace('a','e')); //ReplaceAll
            StringBuilder sb = new StringBuilder("deneme",5); //capacity verilen sayı katlarında dinamik olarak arttırılır.
            Console.WriteLine(sb.AppendLine().Append("deneme1"));
            sb=new StringBuilder("Insert");
            Console.WriteLine(sb.ToString());
            sb.Insert(0, "Sentence: ");
            Console.WriteLine(sb);

            // "Sort"
            char[] arrar = {'h', 'c', 'y'};
            Array.Sort(arrar);
            Console.WriteLine(string.Join(',',arrar));
            var result = from item in arrar orderby item descending select item;
            Console.WriteLine(string.Join(',',result));

            var list = new List<char>() { 'f','w','v'};
            list.Sort();
            Console.WriteLine(string.Join(',',list));

            // "Split"
            string data = "there is a cat";
            string[] strings = data.Split(' ');
            Console.WriteLine(string.Join(',',strings));
            strings = Regex.Split(data, @"\W+");
            Console.WriteLine(string.Join(',', strings));

            Console.Read();
        }
    }
}
