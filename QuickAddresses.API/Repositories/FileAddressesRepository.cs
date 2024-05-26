using QuickAddresses.Models.Domain;

namespace QuickAddresses.Repositories
{
    public class FileAddressesRepository : IAddressesRepository
    {
        private readonly string _path = AppDomain.CurrentDomain.BaseDirectory;

        public async Task AddAddress(Address address)
        {
            using FileStream fs = File.Open($"{_path}/addresses.txt", FileMode.Append);
            using StreamWriter sw = new(fs);

            await sw.WriteLineAsync($"{address.Id}\t{address.Name}\t{address.Surname}\t{address.Email}\t{address.Phone}\t{address.City}");
        }

        public async Task<List<Address>> GetAddressesFromCity(string city)
        {
            using FileStream fs = File.Open($"{_path}/addresses.txt", FileMode.OpenOrCreate);
            using StreamReader sr = new(fs);
            
            string line;
            List<string> lines = [];
            List<Address> addresses = [];
            while (sr.EndOfStream == false)
            {
                line = await sr.ReadLineAsync();
                string[] fields = line.Split('\t');
                if (fields[5] == city)
                {
                    var address = new Address
                    {
                        Id = new Guid(fields[0]),
                        Name = fields[1],
                        Surname = string.IsNullOrEmpty(fields[2]) ? null : fields[2],
                        Email = string.IsNullOrEmpty(fields[3]) ? null : fields[3],
                        Phone = string.IsNullOrEmpty(fields[4]) ? null : fields[4],
                        City = string.IsNullOrEmpty(fields[5]) ? null : fields[5]
                    };
                    addresses.Add(address);
                }
            }

            return addresses;
        }

        public async Task<Address?> GetLastAddress()
        {
            using FileStream fs = File.Open($"{_path}/addresses.txt", FileMode.OpenOrCreate);
            using StreamReader sr = new(fs);

            string? line = "";
            while (sr.EndOfStream == false)
            {
                line = await sr.ReadLineAsync();
            }

            if (string.IsNullOrEmpty(line))
            {
                return null;
            }
            string[] fields = line.Split('\t');

            return new Address
            {
                Id = new Guid(fields[0]),
                Name = fields[1],
                Surname = string.IsNullOrEmpty(fields[2]) ? null : fields[2],
                Email = string.IsNullOrEmpty(fields[3]) ? null : fields[3],
                Phone = string.IsNullOrEmpty(fields[4]) ? null : fields[4],
                City = string.IsNullOrEmpty(fields[5]) ? null : fields[5]
            };
        }
    }
}
