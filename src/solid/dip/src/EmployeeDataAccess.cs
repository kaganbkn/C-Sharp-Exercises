using System;
using System.Collections.Generic;
using System.Text;

namespace dip.src
{
    public interface IEmployeeDataAccess
    {
        Employee GetEmployeeDetails(int id);
    }

    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        public Employee GetEmployeeDetails(int id)
        {
            // In real time get the employee details from db
            //but here we are hard coded the employee details
            Employee emp = new Employee()
            {
                ID = id,
                Name = "Pranaya",
                Department = "IT",
                Salary = 10000
            };
            return emp;
        }
    }
}
