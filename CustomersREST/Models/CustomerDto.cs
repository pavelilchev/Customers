namespace CustomersREST.Models
{
    using System;

    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int LocationId { get; set; }
    }
}
