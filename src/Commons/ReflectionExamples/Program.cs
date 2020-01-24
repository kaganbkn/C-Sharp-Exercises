using System;
using ReflectionExamples.Data;

namespace ReflectionExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //runtime scenario
            var classType = (typeof(Calculate));
            Calculate calculateInstance = (Calculate)Activator.CreateInstance(classType, 0, 0); //constructor parametre gerektiriyor çünkü //runtime'da yeni bir obje oluşturmuş olduk

            Console.WriteLine(calculateInstance.Add(3, 5));

            Console.WriteLine(calculateInstance.GetType().GetMethod("Multiplication").Invoke(calculateInstance, new object?[] { 31, 61 }));

            Console.Read();
        }
    }
}
