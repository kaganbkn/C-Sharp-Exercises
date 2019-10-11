using System;
using System.Collections.Generic;
using System.Globalization;

namespace KVPair
{
    class Program
    {
        static void Main(string[] args)
        {
            args=new [] { "asd"};

            foreach (var arg in args)
            {
                Console.WriteLine($"Args : {arg}");
            }










            var number = new List<KeyValuePair<string,int>>();
            var obj=new Program();
            //obj.Write();


            Console.Read();
        }

        public void Write()
        {
            Console.WriteLine("ads");
        }
    }
}
