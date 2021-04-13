namespace CustomersREST.Profiles
{
    using AutoMapper;
    using CustomersREST.Database.Entities;
    using CustomersREST.Models;

    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}
