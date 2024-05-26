using QuickAddresses.Models.Domain;

namespace QuickAddresses.Repositories
{
    public interface IAddressesRepository
    {
        public Task<Address?> GetLastAddress();
        public Task<List<Address>> GetAddressesFromCity(string city);
        public Task AddAddress(Address address);
    }
}
