using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AttributeExamples
{
    [ToTable("Customer")]
    class AttrExample
    {
        [Required]
        public int Id { get; set; }
        [RequiredProperty]
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        [Obsolete("Don't use Add(),instead use AddNew method.")]
        public static void Add(AttrExample customer)
        {
            Console.WriteLine(customer.Name + "," + customer.LastName);
        }
        public static void AddNew(AttrExample customer)
        {
            Console.WriteLine(customer.Name + "," + customer.LastName);
        }
    }
}
