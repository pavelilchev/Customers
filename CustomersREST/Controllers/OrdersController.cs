namespace CustomersREST.Controllers
{
    using AutoMapper;
    using CustomersREST.Database.Entities;
    using CustomersREST.Models;
    using CustomersREST.Services;
    using Microsoft.AspNetCore.JsonPatch;
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
                return this.NotFound();
            }

            IEnumerable<Order> orders = this.customersRepository.GetOrders(customerId);

            return this.Ok(this.mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("{orderId}", Name = "GetOrderForCustomer")]
        public ActionResult<IEnumerable<OrderDto>> GetOrderForCustomer(Guid customerId, Guid orderId)
        {
            if (!this.customersRepository.CustomerExists(customerId))
            {
                return this.NotFound();
            }

            var order = this.customersRepository.GetOrder(customerId, orderId);
            if (order == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.mapper.Map<OrderDto>(order));
        }


        [HttpPost]
        public ActionResult<OrderDto> CreateOrderForCustomer(Guid customerId, OrderForCreationDto order)
        {
            if (!this.customersRepository.CustomerExists(customerId))
            {
                return this.NotFound();
            }

            var orderEntiti = this.mapper.Map<Order>(order);
            this.customersRepository.AddOrder(customerId, orderEntiti);

            var orderToReturn = this.mapper.Map<OrderDto>(orderEntiti);
            return this.CreatedAtRoute("GetOrderForCustomer",
                new
                {
                    customerId = customerId,
                    orderId = orderEntiti.Id,
                },
                orderToReturn);
        }

        [HttpPut("{orderId}")]
        public IActionResult UpdateOrderForCustomer(Guid customerId, Guid orderId, OrderForUpdateDto order)
        {
            if (!this.customersRepository.CustomerExists(customerId))
            {
                return this.NotFound();
            }

            var orderForCustomer = this.customersRepository.GetOrder(customerId, orderId);

            if (orderForCustomer == null)
            {
                var orderToAdd = this.mapper.Map<Order>(order);
                orderToAdd.Id = orderId;

                this.customersRepository.AddOrder(customerId, orderToAdd);

                this.customersRepository.Save();

                var orderToReturn = this.mapper.Map<OrderDto>(orderToAdd);

                return CreatedAtRoute("GetOrderForCustomer",
                    new { customerId,orderId = orderToReturn.Id },
                    orderToReturn);
            }

            this.mapper.Map(order, orderForCustomer);

            this.customersRepository.UpdateOrder(orderForCustomer);

            this.customersRepository.Save();
            return this.NoContent();
        }

        [HttpPatch("{orderId}")]
        public ActionResult PartiallyUpdateOrderForCustomer(Guid customerId, Guid OrderId, JsonPatchDocument<OrderForUpdateDto> patchDocument)
        {
            if (!this.customersRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var orderForCustomerFromRepo = this.customersRepository.GetOrder(customerId, OrderId);

            if (orderForCustomerFromRepo == null)
            {
                var orderDto = new OrderForUpdateDto();
                patchDocument.ApplyTo(orderDto, ModelState);

                if (!TryValidateModel(orderDto))
                {
                    return ValidationProblem(ModelState);
                }

                var orderToAdd = this.mapper.Map<Order>(orderDto);
                orderToAdd.Id = OrderId;

                this.customersRepository.AddOrder(customerId, orderToAdd);
                this.customersRepository.Save();

                var orderToReturn = this.mapper.Map<OrderDto>(orderToAdd);

                return CreatedAtRoute("GetOrderForCustomer",
                    new { customerId, orderId = orderToReturn.Id },
                    orderToReturn);
            }

            var orderToPatch = this.mapper.Map<OrderForUpdateDto>(orderForCustomerFromRepo);       
            patchDocument.ApplyTo(orderToPatch, ModelState);

            if (!TryValidateModel(orderToPatch))
            {
                return ValidationProblem(ModelState);
            }

            this.mapper.Map(orderToPatch, orderForCustomerFromRepo);

            this.customersRepository.UpdateOrder(orderForCustomerFromRepo);

            this.customersRepository.Save();

            return NoContent();
        }
    }
}
