using AutoMapper;
using ContactBook.API.Model.Domain;
using ContactBook.API.Model.DTO;

namespace ContactBook.API.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<AddAppUserDto, AppUser>().ReverseMap();
        }
    }
}
