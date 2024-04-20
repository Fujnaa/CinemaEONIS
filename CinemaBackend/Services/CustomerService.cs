using AutoMapper;
using CinemaBackend.Models;
using CinemaBackend.Models.DTOs.CustomerDTOs;
using Microsoft.EntityFrameworkCore;

namespace CinemaBackend.Services
{
    public class CustomerService : ICustomerService
    {

        private CinemaDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerService(CinemaDatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            try
            {
                return await _dbContext.Customers.Include(c => c.Tickets).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Customer> GetCustomerById(Guid customerId)
        {
            try
            {
                Customer? search = await _dbContext.Customers.FirstOrDefaultAsync(w => w.CustomerId == customerId);

                if (search == null)
                    throw new KeyNotFoundException();

                return search;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {

            var createdCustomer = await _dbContext.AddAsync(customer);

            await _dbContext.SaveChangesAsync();

            return createdCustomer.Entity;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            try
            {
                var toUpdate = await _dbContext.Customers.FirstOrDefaultAsync(w => w.CustomerId == customer.CustomerId);

                if (toUpdate == null)
                    throw new KeyNotFoundException();

                toUpdate.CustomerId = customer.CustomerId;
                toUpdate.CustomerName = customer.CustomerName;
                toUpdate.CustomerEmailAdress = customer.CustomerEmailAdress;
                toUpdate.CustomerPhoneNumber = customer.CustomerPhoneNumber;
                toUpdate.CustomerMembershipLevel = customer.CustomerMembershipLevel;

                toUpdate.CustomerId = customer.CustomerId;

                await _dbContext.SaveChangesAsync();

                return toUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteCustomer(Guid customerId)
        {
            try
            {
                Customer? search = await _dbContext.Customers.FirstOrDefaultAsync(w => w.CustomerId == customerId);

                if (search == null)
                    throw new KeyNotFoundException();

                _dbContext.Customers.Remove(search);

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
