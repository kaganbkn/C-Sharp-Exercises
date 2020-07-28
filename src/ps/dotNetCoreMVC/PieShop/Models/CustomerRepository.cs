using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetCustomer(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _appDbContext.Customers;
        }

        public Customer GetCustomer(int id)
        {
            return _appDbContext.Customers.FirstOrDefault(c=>c.Id==id);
        }
    }
}
