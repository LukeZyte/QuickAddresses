using AutoMapper;
using QuickAddresses.Models.Domain;
using QuickAddresses.Models.Dtos;
using QuickAddresses.Repositories;

namespace QuickAddresses.Services
{
    public class AddressService(IAddressesRepository addressesRepository, IMapper mapper) : IAddressService
    {
        public async Task<AddressDto?> GetLast()
        {
            var address = await addressesRepository.GetLastAddress();
            if (address is null)
            {
                return null;
            }
            var addressDto = mapper.Map<AddressDto>(address);
            return addressDto;
        }

        public async Task<List<AddressDto>> GetFromCity(string city) 
        { 
            var addresses = await addressesRepository.GetAddressesFromCity(city);
            var addressesDto = mapper.Map<List<AddressDto>>(addresses);
            return addressesDto;
        }

        public async Task AddAddress(AddAddressRequestDto addAddressRequestDto)
        {
            //var address = mapper.Map<Address>(addAddressRequestDto);
            var address = new Address
            {
                Id = Guid.NewGuid(),
                Name = addAddressRequestDto.Name,
                Surname = addAddressRequestDto.Surname,
                City = addAddressRequestDto.City,
                Email = addAddressRequestDto.Email,
                Phone = addAddressRequestDto.Phone,
            };
            await addressesRepository.AddAddress(address); 
        }
    }
}
