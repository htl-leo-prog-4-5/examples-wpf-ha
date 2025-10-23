using Core.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelAgency", Version = "v1" });

    var xmlFile = "WebApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString))
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddTransient<ITripRepository, TripRepository>()
    .AddTransient<IRouteRepository, RouteRepository>()
    .AddTransient<IRouteStepRepository, RouteStepRepository>()
    .AddTransient<IHotelRepository, HotelRepository>()
    .AddTransient<IPlaneRepository, PlaneRepository>()
    .AddTransient<IShipRepository, ShipRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true) // app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add CORS to support Single Page Apps (SPAs)
app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();