namespace CustomersREST.Controllers
{
    using AutoMapper;
    using CustomersREST.Database.Entities;
    using CustomersREST.Helpers.CustomModelBinders;
    using CustomersREST.Models;
    using CustomersREST.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/customercollections")]
    [ApiController]
    public class CustomerCollectionsController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;
        private readonly IMapper mapper;

        public CustomerCollectionsController(ICustomersRepository customersContext, IMapper mapper)
        {
            this.customersRepository = customersContext ?? throw new ArgumentNullException(nameof(customersContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetCustomerCollection")]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomerCollection([FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var customersEntities = this.customersRepository.GetCustomers(ids);
            if (ids.Count() != customersEntities.Count())
            {
                return NotFound();
            }

            var customersToReturn = this.mapper.Map<IEnumerable<CustomerDto>>(customersEntities);

            return Ok(customersToReturn);
        }


        [HttpPost]
        public ActionResult<IEnumerable<CustomerDto>> CreateCustomerCollection(IEnumerable<CustomerForCreationDto> customerCollection)
        {
            var customerEntities = this.mapper.Map<IEnumerable<Customer>>(customerCollection);
            foreach (var customer in customerEntities)
            {
                this.customersRepository.AddCustomer(customer);
            }

            this.customersRepository.Save();

            var customerCollectionToReturn = this.mapper.Map<IEnumerable<CustomerDto>>(customerEntities);

            return CreatedAtRoute("GetCustomerCollection",
                new
                {
                    ids = string.Join(",", customerCollectionToReturn.Select(c => c.Id))
                },
                customerCollectionToReturn);
        }
    }
}
