namespace CustomersREST.Services
{
    using CustomersREST.Database.Entities;
    using CustomersREST.ResourseParameters;
    using System;
    using System.Collections.Generic;

    public interface ICustomersRepository
    {
        void AddCustomer(Customer customer);

        void AddVehicle(Guid customerId, Vehicle vehicle);

        bool CustomerExists(Guid customerId);

        void DeleteCustomer(Customer customer);

        void DeleteVehicle(Vehicle vehicle);

        void Dispose();

        Customer GetCustomer(Guid customerId);

        IEnumerable<Customer> GetCustomers();

        IEnumerable<Customer> GetCustomers(CustomersResourcesParameters customersResourcesParameters);

        Vehicle GetVehicle(Guid CustomerId, Guid vehicleId);

        IEnumerable<Vehicle> GetVehicles(Guid CustomerId);

        IEnumerable<Order> GetOrders(Guid customerId);

        Order GetOrder(Guid customerId, Guid orderId);

        bool Save();
    }
}