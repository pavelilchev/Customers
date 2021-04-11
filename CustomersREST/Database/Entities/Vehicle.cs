namespace CustomersREST.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Make { get; set; }

        [Required]
        public int Mileage { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public Guid CustomerId { get; set; }
    }
}
