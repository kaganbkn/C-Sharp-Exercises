using System;
using System.Collections.Generic;
using System.Text;

namespace Polymorphism.Interfaces
{
    public interface IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public void Add();

        public void TryDeclareMethod()
        {
            Console.WriteLine("asd");
        }

        //public void TryMethod();
        //An interface is a contract for instances.
    }
}
