using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Polymorphism.Interfaces;

namespace Polymorphism.Data
{
    public class Manager : IPerson
    {
        public int Id { get ; set ; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int NumberOfManaged { get; set; }

        public void Add()
        {
            Console.WriteLine("Manager Added");
        }
    }
}
