using System;
using System.Text.Json;
using dip.src;

namespace dip
{
    class Program
    {
        static void Main(string[] args)
        {
            //The Dependency Inversion Principle (DIP) states that high-level modules/classes should not depend on low-level modules/classes.
            //Both should depend upon abstractions. Secondly, abstractions should not depend upon details. Details should depend upon abstractions.
            //dependency inversion principle
            var employee = new EmployeeBusinessLogic();
            Console.WriteLine(JsonSerializer.Serialize(employee.GetEmployeeDetails(1)));

            //dependency inversion principle

            //liskov substitution principle : This principle states that, if S is a subtype of T, then objects of type T should be replaced with the objects of type S.
            // yerine koyma

            Apple apple = new Orange();
            Console.WriteLine(apple.GetColor());

            //after

            Fruit fruit = new Avocado();
            Console.WriteLine(fruit.GetColor());
            fruit = new Banana();
            Console.WriteLine(fruit.GetColor());

            //liskov substitution principle

            //open close principle

            var invoice =new Invoice();
            Console.WriteLine(invoice.GetInvoiceDiscount(1000, InvoiceType.FinalInvoice));
            Console.WriteLine(invoice.GetInvoiceDiscount(1000, InvoiceType.ProposedInvoice));

            //after

            InvoiceOCP fInvoice = new FinalInvoice();
            InvoiceOCP pInvoice = new ProposedInvoice();
            InvoiceOCP rInvoice = new RecurringInvoice();
            Console.WriteLine(fInvoice.GetInvoiceDiscount(100));
            Console.WriteLine(pInvoice.GetInvoiceDiscount(100));
            Console.WriteLine(rInvoice.GetInvoiceDiscount(100));

            //open close principle


            Console.Read();

            // https://dotnettutorials.net/lesson/dependency-inversion-principle/
        }
    }
}
