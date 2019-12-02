using System;
using System.Reflection;

namespace AttributeExamples
{
    class Program
    {
        public enum Developers
        {
            A,
            B,
            C
        }
        [AttributeUsage(AttributeTargets.Method)] //,AllowMultiple =true
        public class DeveloperInfo : Attribute
        {
            public Developers _developers { get; set; }
            public string Version { get; set; }
        }
        public class Development
        {
            [DeveloperInfo(_developers =Developers.A,Version ="1.0.1")]
            public int Odd()
            {
                return 1;
            }
            [DeveloperInfo(_developers = Developers.C, Version = "1.0.2")]
            public int Even()
            {
                return 2;
            }
        }
        static void Main(string[] args)
        {
            MethodInfo[] info = typeof(Development).GetMethods(); //reflection
            foreach (var item in info)
            {
                Console.WriteLine("Metod Adı: " + item.Name);
                Console.WriteLine("{");

                DeveloperInfo[] attributes = (DeveloperInfo[])item.GetCustomAttributes(typeof(DeveloperInfo));

                foreach (DeveloperInfo attr in attributes)
                {
                    Console.WriteLine("Geliştirici: " + attr._developers);
                    Console.WriteLine("Release Sürüm: " + attr.Version);
                }

                Console.WriteLine("}");
            }

            Console.Read();
        }
    }
}
