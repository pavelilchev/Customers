namespace CustomersREST.Models
{
    using System;

    public class OrderForUpdateDto
    {
        public DateTime CloseDate { get; set; }

        public decimal Total { get; set; }

        public Guid? VehicleId { get; set; }
    }
}
