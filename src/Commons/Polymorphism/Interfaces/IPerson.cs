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

        //An interface is a contract for instances.
    }
}
