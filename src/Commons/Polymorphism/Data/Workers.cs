using System;
using System.Collections.Generic;
using System.Text;
using Polymorphism.Interfaces;

namespace Polymorphism.Data
{
    public class Workers :IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }

        public void Add()
        {
            Console.WriteLine("Workers Added.");
        }
    }
}
