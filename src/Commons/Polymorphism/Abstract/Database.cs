using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Polymorphism.Abstract
{
    public abstract class Database
    {
        public void Add()
        {
            Console.WriteLine("Added default.");
        }

        public abstract void Delete(); // (abstracts and interfaces)it should be public because we will use for overriding.
    }

    public class SqlServer : Database
    {
        public override void Delete()
        {
            Console.WriteLine("Deleted SqlServer.");
        }

        ///Add more function....
    }
    public class Oracle : Database
    {
        public override void Delete()
        {
            Console.WriteLine("Deleted Oracle.");
        }
    }

}
