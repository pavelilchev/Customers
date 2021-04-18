using System.Collections.Generic;

namespace CustomersREST.Models
{
    public class CustomerForCreationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int LocationId { get; set; }

        public ICollection<OrderForCreationDto> Orders { get; set; } = new List<OrderForCreationDto>();
    }
}
