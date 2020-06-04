using System;
using System.Text.Json;
using dip.src;

namespace dip
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee = new EmployeeBusinessLogic();
            Console.WriteLine(JsonSerializer.Serialize(employee.GetEmployeeDetails(1)));

            Console.Read();
        }
    }
}
