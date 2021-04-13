namespace CustomersREST.Models
{
    using System;

    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime CloseDate { get; set; }

        public decimal Total { get; set; }

        public Guid CustomerId { get; set; }

        public Guid VehicleId { get; set; }
    }
}
