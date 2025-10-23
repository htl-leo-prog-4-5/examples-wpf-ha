using Core.Contracts;

using Microsoft.EntityFrameworkCore;

using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString))
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddTransient<ITicketRepository, TicketRepository>()
    .AddTransient<IGameRepository, GameRepository>()
    .AddTransient<ITipRepository, TipRepository>()
    .AddTransient<IOfficeRepository, OfficeRepository>()
    .AddTransient<IStateRepository, StateRepository>()
    .AddTransient<IImportService, ImportService>()
    .AddTransient<ICreateTicketService, CreateTicketService>()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
    await context.Database.EnsureCreatedAsync();
}

app.Run();