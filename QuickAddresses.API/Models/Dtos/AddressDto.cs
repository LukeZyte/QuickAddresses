namespace QuickAddresses.Models.Dtos
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
    }
}
