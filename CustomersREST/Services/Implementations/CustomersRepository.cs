namespace CustomersREST.Services.Implementations
{
    using CustomersREST.Database.Context;
    using CustomersREST.Database.Entities;
    using CustomersREST.ResourseParameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomersRepository : ICustomersRepository, IDisposable
    {
        private readonly CustomersContext context;

        public CustomersRepository(CustomersContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            customer.Id = Guid.NewGuid();

            foreach (var order in customer.Orders)
            {
                order.Id = Guid.NewGuid();
            }

            this.context.Customers.Add(customer);
        }

        public bool CustomerExists(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            return this.context.Customers.Any(a => a.Id == customerId);
        }

        public void DeleteCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            this.context.Customers.Remove(customer);
        }

        public Customer GetCustomer(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            return this.context.Customers.FirstOrDefault(a => a.Id == customerId);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return this.context.Customers.ToList();
        }

        public IEnumerable<Customer> GetCustomers(IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            return this.context.Customers.Where(c => ids.Contains(c.Id)).ToList();
        }

        public IEnumerable<Customer> GetCustomers(CustomersResourcesParameters customersResourcesParameters)
        {
            if (!customersResourcesParameters.Locationid.HasValue && 
                string.IsNullOrWhiteSpace(customersResourcesParameters.Search))
            {
                return this.GetCustomers();
            }

            var customers = this.context.Customers as IQueryable<Customer>;
            if (customersResourcesParameters.Locationid.HasValue)
            {
                customers = customers.Where(c => c.LocationId == customersResourcesParameters.Locationid);
            }

            if (!string.IsNullOrWhiteSpace(customersResourcesParameters.Search))
            {
                var search = customersResourcesParameters.Search.Trim();
                customers = customers.Where(c => c.FirstName.Contains(search) || 
                c.LastName.Contains(search) || 
                c.Email.Contains(search));
            }

            return customers.ToList();
        }

        public void AddVehicle(Guid customerId, Vehicle vehicle)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            vehicle.CustomerId = customerId;
            this.context.Vehicles.Add(vehicle);
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            this.context.Vehicles.Remove(vehicle);
        }

        public Vehicle GetVehicle(Guid CustomerId, Guid vehicleId)
        {
            if (CustomerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(CustomerId));
            }

            if (vehicleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(vehicleId));
            }

            return this.context
                .Vehicles
                .Where(c => c.CustomerId == CustomerId && c.Id == vehicleId).FirstOrDefault();
        }

        public IEnumerable<Vehicle> GetVehicles(Guid CustomerId)
        {
            if (CustomerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(CustomerId));
            }

            return this.context
                .Vehicles
                .Where(c => c.CustomerId == CustomerId)
                .ToList();
        }

        public IEnumerable<Order> GetOrders(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            return this.context
                .Orders
                .Where(c => c.CustomerId == customerId)
                .OrderBy(c => c.CloseDate)
                .ToList();
        }

        public Order GetOrder(Guid customerId, Guid orderId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            if (orderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            return this.context
                .Orders
                .FirstOrDefault(o => o.CustomerId == customerId && o.Id == orderId);
        }

        public void AddOrder(Guid customerId, Order order)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.CustomerId = customerId;
            this.context.Orders.Add(order);
        }

        public void UpdateOrder(Order ordersForCustomer)
        {
        }

        public bool Save()
        {
            return this.context.SaveChanges() >= 0;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
