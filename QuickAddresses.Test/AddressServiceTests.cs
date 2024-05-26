using AutoMapper;
using Moq;
using QuickAddresses.Models.Domain;
using QuickAddresses.Models.Dtos;
using QuickAddresses.Repositories;
using QuickAddresses.Services;

namespace QuickAddresses.Tests
{
    [TestClass]
    public class AddressServiceTests
    {
        private Mock<IAddressesRepository> _addressesRepository;
        private Mock<IMapper> _mapper;
        private AddressService _addressService;

        [TestInitialize]
        public void SetUp()
        {
            _addressesRepository = new Mock<IAddressesRepository>();
            _mapper = new Mock<IMapper>();
            _addressService = new AddressService(_addressesRepository.Object, _mapper.Object);
        }

        [TestMethod]
        public async Task TestGetLast_AddressOk()
        {
            var address = new Address
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "Email",
                Phone = "Phone",
                City = "City"
            };
            var addressDto = new AddressDto
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "Email",
                Phone = "Phone",
                City = "City"
            };

            _addressesRepository.Setup(x => x.GetLastAddress()).ReturnsAsync(address);
            _mapper.Setup(m => m.Map<AddressDto>(address)).Returns(addressDto);

            var result = await _addressService.GetLast();

            Assert.IsNotNull(result);
            Assert.AreEqual(addressDto, result);
        }

        [TestMethod]
        public async Task TestGetLast_AddressIsNull()
        {
            var address = new Address
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "Email",
                Phone = "Phone",
                City = "City"
            };
            var addressDto = new AddressDto
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "Email",
                Phone = "Phone",
                City = "City"
            };
            _addressesRepository.Setup(x => x.GetLastAddress()).ReturnsAsync((Address)null);
            _mapper.Setup(m => m.Map<AddressDto>(address)).Returns(addressDto);
            var result = await _addressService.GetLast();

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestGetFromCity()
        {
            const string city = "City";
            var addresses = new List<Address>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Surname = "Surname",
                    Email = "Email",
                    Phone = "Phone",
                    City = "City"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Surname = "Surname",
                    Email = "Email",
                    Phone = "Phone",
                    City = "City"
                }
            };

            var addressesDto = new List<AddressDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Surname = "Surname",
                    Email = "Email",
                    Phone = "Phone",
                    City = "City"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Surname = "Surname",
                    Email = "Email",
                    Phone = "Phone",
                    City = "City"
                }
            };


            _addressesRepository.Setup(x => x.GetAddressesFromCity(city)).ReturnsAsync(addresses);
            _mapper.Setup(m => m.Map<List<AddressDto>>(addresses)).Returns(addressesDto);

            var result = await _addressService.GetFromCity(city);

            Assert.IsNotNull(result);
            Assert.AreEqual(addressesDto.Count, result.Count);
        }
    }
}
