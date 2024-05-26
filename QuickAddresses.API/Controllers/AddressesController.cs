using Microsoft.AspNetCore.Mvc;
using QuickAddresses.Models.Dtos;
using QuickAddresses.Services;

namespace QuickAddresses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController(IAddressService addressService) : ControllerBase
    {

        // GET last Address
        [HttpGet]
        public async Task<IActionResult> GetLast() 
        {
            var address = await addressService.GetLast();
            if (address is null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        //GET all addresses from provided city
        [HttpGet]
        [Route("{city}")]
        public async Task<IActionResult> GetFromCity([FromRoute] string city)
        {
            var addressesDto = await addressService.GetFromCity(city);
            if (addressesDto.Count < 1)
            {
                return NotFound();
            }

            return Ok(addressesDto);
        }

        //POST a new address
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressRequestDto addAddressRequestDto) 
        {
            await addressService.AddAddress(addAddressRequestDto);

            return Ok();
        }
    }
}
