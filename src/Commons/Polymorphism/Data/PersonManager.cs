using System;
using System.Collections.Generic;
using System.Text;
using Polymorphism.Interfaces;

namespace Polymorphism.Data
{
    public class PersonManager
    {
        public void WriteName(IPerson person)
        {
            Console.WriteLine(person.Name);
        }

        public void Add(IPerson person)
        {
            person.Add();
        }
    }
}
