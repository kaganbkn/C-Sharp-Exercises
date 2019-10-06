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

            Console.ReadLine();
        }
    }
}
