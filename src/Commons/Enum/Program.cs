using System;

namespace Enum
{
    class Program
    {
        [Flags] //we use this for bitwise operation in enum
        enum RenderType
        {
            None = 0x0,
            DataUri = 0x1,
            GZip = 0x2,
            ContentPage = 0x4,
            ViewPage = 0x8,
            HomePage = 0x10 // Next two values could be 0x20, 0x40
        }
        enum Importance //:byte // if we use byte ,max number of enum elements will be 256 ,normally int 
        {
            None,
            Important,
            Critical=5,
            After
        }
        static void Main(string[] args)
        {
            var temp = Importance.Important;
            if (temp == Importance.Important)
            {
                Console.WriteLine(temp);
            }
            Importance importance = Importance.Critical;
            Console.WriteLine(importance);
            Console.WriteLine((int)importance);
            Console.WriteLine(importance.ToString() == "Critical");
            Console.WriteLine((int)Importance.After);


            //bitwise
            Console.WriteLine("Bitwise");
            var a = 'i';
            var b = a >> 1; //shift
            Console.WriteLine((char)b);
            Console.WriteLine((decimal)b);
            b = a | 1;  //or
            Console.WriteLine((char)b);
            Console.WriteLine((decimal)b);
            b = a & 'a';  //and
            Console.WriteLine((char)b);
            Console.WriteLine((decimal)b);



            // "Switch-case"

            switch (importance)
            {
                case Importance.None:
                case Importance.After:
                {
                    Console.WriteLine("OK");
                    break;
                }
                case Importance.Critical:
                case Importance.Important:
                {
                    Console.WriteLine("NO");
                    break;
                }
                default:
                {
                    Console.WriteLine("NO"); 
                    break;
                }
            }

            // "Tuple"

            var tuple=new Tuple<int,string,bool>(3,"three",true);
            var tuples=new Tuple<int, int, int>[2];
            Console.WriteLine(tuple.Item1);

            Console.ReadLine();
        }
    }
}
