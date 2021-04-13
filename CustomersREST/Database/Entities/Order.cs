namespace CustomersREST.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CloseDate { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Total { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
               
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        [Required]
        public Guid VehicleId { get; set; }
    }
}
