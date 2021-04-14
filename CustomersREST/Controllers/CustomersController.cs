﻿namespace CustomersREST.Controllers
{
    using AutoMapper;
    using CustomersREST.Database.Entities;
    using CustomersREST.Models;
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
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customers = this.customersRepository.GetCustomers();

            return Ok(this.mapper.Map<IEnumerable<CustomerDto>>(customers));
        }

        [HttpGet("{customerId}", Name = "GetCustomer")]
        [HttpHead]
        public ActionResult<CustomerDto> GetCustomer(Guid customerId)
        {
            var customer = this.customersRepository.GetCustomer(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<CustomerDto>(customer));
        }
    }
}
