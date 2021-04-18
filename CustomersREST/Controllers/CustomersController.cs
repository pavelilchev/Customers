namespace CustomersREST.Controllers
{
    using AutoMapper;
    using CustomersREST.Database.Entities;
    using CustomersREST.Models;
    using CustomersREST.ResourseParameters;
    using CustomersREST.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;
        private readonly IMapper mapper;

        public CustomersController(ICustomersRepository customersContext, IMapper mapper)
        {
            this.customersRepository = customersContext ?? throw new ArgumentNullException(nameof(customersContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers([FromQuery]CustomersResourcesParameters customersResourcesParameters)
        {
            var customers = this.customersRepository.GetCustomers(customersResourcesParameters);

            return Ok(this.mapper.Map<IEnumerable<CustomerDto>>(customers));
        }

        [HttpGet("{customerId}", Name = "GetCustomer")]
        public ActionResult<CustomerDto> GetCustomer(Guid customerId)
        {
            var customer = this.customersRepository.GetCustomer(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public ActionResult<CustomerDto> CreateCustomer(CustomerForCreationDto customer)
        {
            var customerEntities = this.mapper.Map<Customer>(customer);
            this.customersRepository.AddCustomer(customerEntities);
            this.customersRepository.Save();

            var customerToReturn = this.mapper.Map<CustomerDto>(customerEntities);

            return CreatedAtRoute("GetCustomer",
                new
                {
                    customerId = customerToReturn.Id
                },
                customerToReturn);
        }

        [HttpOptions]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomersOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");

            return Ok();
        }
    }
}
