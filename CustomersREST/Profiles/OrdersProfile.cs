namespace CustomersREST.Profiles
{
    using AutoMapper;
    using CustomersREST.Database.Entities;
    using CustomersREST.Models;
    using System;

    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(
                      dest => dest.VehicleId,
                      opt => opt.MapFrom(src => src.VehicleId.HasValue ? src.VehicleId.Value : (Guid?)null));

            CreateMap<OrderForCreationDto, Order>()
                 .ForMember(
                      dest => dest.VehicleId,
                      opt => opt.MapFrom(src => src.VehicleId.HasValue ? src.VehicleId.Value : (Guid?)null));
        }
    }
}
