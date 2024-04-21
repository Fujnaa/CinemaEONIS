using CinemaBackend.Models;
using CinemaBackend.Models.DTOs.CustomerDTOs;

namespace CinemaBackend.Services
{
    public interface ICustomerService
    {

        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(Guid customerId);
        Task<Customer> GetCustomerByEmail(String customerEmail);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task DeleteCustomer(Guid customerId);

    }
}
