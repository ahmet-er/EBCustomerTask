using AutoMapper;
using EBCustomerTask.Application.DTOs;
using EBCustomerTask.Core.Entities;

namespace EBCustomerTask.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region AppUser Maps
            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<AppUser, RegisterViewModel>();

            CreateMap<LoginViewModel, AppUser>();
            #endregion

            #region Customer Maps
            CreateMap<Customer, CustomerDetailViewModel>();

            CreateMap<Customer, CustomerGetAllViewModel>();

            CreateMap<CustomerCreateViewModel, Customer>();

            CreateMap<CustomerUpdateViewModel, Customer>();
            #endregion
        }
    }
}
