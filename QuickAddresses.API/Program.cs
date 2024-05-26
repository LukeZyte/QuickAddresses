using QuickAddresses.Repositories;
using QuickAddresses.Mappings;
using QuickAddresses.Services;

var builder = WebApplication.CreateBuilder(args);

// Logging requests to the console
builder.Services.AddHttpLogging(o => { });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddSingleton<IAddressesRepository, FileAddressesRepository>();

var app = builder.Build();

// Logging requests to the console
app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
