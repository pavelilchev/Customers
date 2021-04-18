namespace CustomersREST.Controllers
{
    using AutoMapper;
    using CustomersREST.Database.Entities;
    using CustomersREST.Models;
    using CustomersREST.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/customers/{customerId}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;
        private readonly IMapper mapper;

        public OrdersController(ICustomersRepository customersContext, IMapper mapper)
        {
            this.customersRepository = customersContext ?? throw new ArgumentNullException(nameof(customersContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<OrderDto>> GetOrdersForCustomer(Guid customerId)
        {
            if (!this.customersRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            IEnumerable<Order> orders = this.customersRepository.GetOrders(customerId);

            return Ok(this.mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("{orderId}")]
        public ActionResult<IEnumerable<OrderDto>> GetOrderForCustomer(Guid customerId, Guid orderId)
        {
            if (!this.customersRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var order = this.customersRepository.GetOrder(customerId, orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<OrderDto>(order));
        }
    }
}
