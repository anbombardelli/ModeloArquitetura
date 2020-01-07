using Arquitetura.Domain.Entities;
using Arquitetura.Domain.ViewModels;
using AutoMapper;

namespace Arquitetura.CrossCutting.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
