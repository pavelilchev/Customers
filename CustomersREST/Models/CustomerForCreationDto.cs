namespace CustomersREST.Models
{
    using CustomersREST.Helpers.ValidationAttributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [FirstAndLastNameMustBeNotAnEmail(ErrorMessage = "First and last name must be different from email.")]
    public class CustomerForCreationDto
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int LocationId { get; set; }

        public ICollection<OrderForCreationDto> Orders { get; set; } = new List<OrderForCreationDto>();
    }
}
