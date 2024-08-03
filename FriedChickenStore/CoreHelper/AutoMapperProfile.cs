using AutoMapper;
using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Model.Entity;

namespace FriedChickenStore.CoreHelper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateTimes, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdateTimes, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateTimes, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdateTimes, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.UserId, opt => opt.Condition(src => src.UserId != 0))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateTimes, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdateTimes, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreateTimes, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdateTimes, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


        }
    }
}
