using AutoMapper;
using CinemaBackend.Models.DTOs.CustomerDTOs;
using CinemaBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;

namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Worker")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
        {
            try
            {
                List<Customer> customers = await _customerService.GetCustomers();

                if (customers == null || customers.Count == 0)
                    return NoContent();

                List<CustomerDto> customersDto = new List<CustomerDto>();

                foreach(var customer in customers) { 
                
                    CustomerDto customerDto = _mapper.Map<CustomerDto>(customer);
                    customersDto.Add(customerDto);
                
                }

                return Ok(customersDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }

        }

        [HttpGet("Admin")]
        public async Task<ActionResult<List<CustomerAdminDto>>> GetCustomersAdmin()
        {
            try
            {
                List<Customer> customers = await _customerService.GetCustomersAdmin();

                if (customers == null || customers.Count == 0)
                    return NoContent();

                List<CustomerAdminDto> customersDto = new List<CustomerAdminDto>();

                foreach (var customer in customers)
                {

                    CustomerAdminDto customerDto = _mapper.Map<CustomerAdminDto>(customer);
                    customersDto.Add(customerDto);

                }

                return Ok(customersDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }

        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(Guid customerId)
        {
            try
            {
                Customer customer = await _customerService.GetCustomerById(customerId);

                if (customer == null)
                    return NotFound();

                CustomerDto customerDto = _mapper.Map<CustomerDto>(customer);

                return Ok(customerDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("Email/{customerEmail}")]
        [AllowAnonymous]
        public async Task<ActionResult<CustomerDto>> GetCustomerByEmail(String customerEmail)
        {
            try
            {
                Customer customer = await _customerService.GetCustomerByEmail(customerEmail);

                if (customer == null)
                    return NotFound();

                CustomerDto customerDto = _mapper.Map<CustomerDto>(customer);

                return Ok(customerDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateDto customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Customer toCreate = _mapper.Map<Customer>(customer);

                Customer createdCustomer = await _customerService.CreateCustomer(toCreate);

                return _mapper.Map<CustomerDto>(createdCustomer);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }
        }

        [HttpPut]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer(CustomerUpdateDto customer)
        {
            try
            {
                Customer toUpdate = _mapper.Map<Customer>(customer);

                Customer updatedCustomer = await _customerService.UpdateCustomer(toUpdate);

                CustomerDto updatedCustomerDto = _mapper.Map<CustomerDto>(updatedCustomer);

                return Ok(updatedCustomerDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer(Guid customerId)
        {
            try
            {

                await _customerService.DeleteCustomer(customerId);
                return Ok();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }
        }
    }
}
