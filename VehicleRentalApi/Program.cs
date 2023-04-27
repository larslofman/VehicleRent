using VehicleRentalApi.TwoWheelers.ManualBike.Repositories;
using VehicleRentalApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IVehicleRentalRepo, VehicleRentalRepo>();
builder.Services.AddScoped<VehicleRentalApi.TwoWheelers.Bike.Repositories.IBikeRepo, BikeRepo>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
