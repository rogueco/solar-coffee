using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var now = DateTime.UtcNow;
            _logger.LogInformation("Creating a new customer");
            customer.CreatedOn = now;
            customer.UpdatedOn = now;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);

            return Ok(newCustomer);
        }

        [HttpGet("/api/customer")]
        public ActionResult GetAllCustomer()
        {
            _logger.LogInformation("Getting all customers");
            var customers = _customerService.GetAllCustomers();
            var customerViewModels = CustomerMapper.SerializeAllCustomers(customers);
            
            return Ok(customerViewModels);
        }

        [HttpDelete("api/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting a customer");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);
        }
    }
}