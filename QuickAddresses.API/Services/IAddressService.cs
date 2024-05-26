using QuickAddresses.Models.Dtos;

namespace QuickAddresses.Services
{
    public interface IAddressService
    {
        public Task<AddressDto?> GetLast();
        public Task<List<AddressDto>> GetFromCity(string city);
        public Task AddAddress(AddAddressRequestDto addAddressRequestDto);
    }
}
