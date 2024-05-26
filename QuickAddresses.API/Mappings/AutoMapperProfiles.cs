using AutoMapper;
using QuickAddresses.Models.Domain;
using QuickAddresses.Models.Dtos;

namespace QuickAddresses.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        { 
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddAddressRequestDto>().ReverseMap();
        }
    }
}
