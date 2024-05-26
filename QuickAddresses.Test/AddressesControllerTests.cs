using Moq;
using QuickAddresses.Controllers;
using QuickAddresses.Models.Dtos;
using QuickAddresses.Services;
using Microsoft.AspNetCore.Mvc;

namespace QuickAddresses.Tests
{
    [TestClass]
    public class AddressesControllerTests
    {
        private Mock<IAddressService> _addressService;
        private AddressesController _addressController;

        [TestInitialize]
        public void SetUp()
        {
            _addressService = new Mock<IAddressService>();
            _addressController = new AddressesController(_addressService.Object);
        }

        [TestMethod]
        public async Task TestGetLast_AddressOk()
        {
            var addressDto = new AddressDto
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "Email",
                Phone = "Phone",
                City = "City"
            };

            _addressService.Setup(x => x.GetLast()).ReturnsAsync(addressDto);

            var getLastResult = await _addressController.GetLast();
            var okResult = getLastResult as OkObjectResult;

            Assert.IsNotNull(getLastResult);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(addressDto, okResult.Value);
        }

        [TestMethod]
        public async Task TestGetLast_AddressIsNull()
        {
            AddressDto? addressDto = null;

            _addressService.Setup(x => x.GetLast()).ReturnsAsync(addressDto);

            var getLastResult = await _addressController.GetLast();
            var notFoundResult = getLastResult as NotFoundResult;

            Assert.IsNotNull(getLastResult);
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task TestGetFromCity_AddressDtoOk()
        {
            var addresses = new List<AddressDto>
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
                },
            };

            _addressService.Setup(x => x.GetFromCity("City")).ReturnsAsync(addresses);

            var getLastResult = await _addressController.GetFromCity("City");
            var okResult = getLastResult as OkObjectResult;

            Assert.IsNotNull(getLastResult);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task TestGetFromCity_AddressDtosIsEmpty()
        {
            List<AddressDto> addressDtos = [];

            _addressService.Setup(x => x.GetFromCity("City")).ReturnsAsync(addressDtos);

            var getFromCityResult = await _addressController.GetFromCity("City");
            var notFoundResult = getFromCityResult as NotFoundResult;

            Assert.IsNotNull(getFromCityResult);
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}