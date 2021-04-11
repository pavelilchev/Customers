namespace CustomersREST.Profiles
{
    using AutoMapper;
    using CustomersREST.Database.Entities;
    using CustomersREST.Models;

    public class CustomersProfile : Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customer, CustomerDto>()
                  .ForMember(
                      dest => dest.Name,
                      opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<CustomerDto, Customer>();
        }
    }
}
