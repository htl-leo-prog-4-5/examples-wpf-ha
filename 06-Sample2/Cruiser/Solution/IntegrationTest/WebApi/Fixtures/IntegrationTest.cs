namespace IntegrationTest.WebApi.Fixtures;

using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;

using Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Base.Core.Contracts;
using Base.Tools.CsvImport;

using Core.Entities;

using Testcontainers.MsSql;

using Xunit;

[Trait("Category", "Integration")]
public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>, IAsyncLifetime
{
    public string ConnectionString { get; private set; }

    private readonly MsSqlContainer       _msSqlContainer = new MsSqlBuilder().Build();
    protected        ApplicationDbContext AppDbContext { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();

        ConnectionString = _msSqlContainer.GetConnectionString();

        AppDbContext = new ApplicationDbContext.ApplicationDbContextFactory(ConnectionString)
            .CreateDbContext([]);

        // Apply migrations or ensure the database is created
        // We do this here, because we don't have an image with the database schema but use the default postgres image
        await AppDbContext.Database.EnsureCreatedAsync();

        // Seed data
        await SeedDataAsync();
    }

    protected virtual async Task SeedDataAsync()
    {
        var shippingCompanies = (await new CsvImport<ShippingCompany>().ReadAsync("WebApi/TestData/ShippingCompany.csv")).ToDictionary(s => s.Id);
        var cruiseShips       = (await new CsvImport<CruiseShip>().ReadAsync("WebApi/TestData/CruiseShip.csv")).ToDictionary(l => l.Id);
        var shipNames         = (await new CsvImport<ShipName>().ReadAsync("WebApi/TestData/ShipName.csv")).ToDictionary(w => w.Id);

        foreach (var shipName in shipNames.Values)
        {
            shipName.CruiseShip   = cruiseShips[shipName.CruiseShipId];
            shipName.CruiseShipId = 0;
        }

        foreach (var cruiseShip in cruiseShips.Values)
        {
            cruiseShip.ShippingCompany   = cruiseShip.ShippingCompanyId.HasValue ? shippingCompanies[cruiseShip.ShippingCompanyId.Value] : null;
            cruiseShip.ShippingCompanyId = null;
        }

        void SetIdNull<T>(IList<T> list) where T : IEntityObject
        {
            foreach (var item in list)
            {
                item.Id = 0;
            }
        }

        SetIdNull(cruiseShips.Values.ToList());
        SetIdNull(shippingCompanies.Values.ToList());
        SetIdNull(shipNames.Values.ToList());

        await AppDbContext.Set<ShippingCompany>().AddRangeAsync(shippingCompanies.Values.ToList());
        await AppDbContext.Set<ShipName>().AddRangeAsync(shipNames.Values.ToList());
        await AppDbContext.Set<CruiseShip>().AddRangeAsync(cruiseShips.Values.ToList());

        await AppDbContext.SaveChangesAsync();
    }

    public Task DisposeAsync()
    {
        return _msSqlContainer.DisposeAsync().AsTask();
    }

    protected readonly ApiWebApplicationFactory _factory;
    protected readonly HttpClient               _client;

    public IntegrationTest(ApiWebApplicationFactory fixture)
    {
        _factory = fixture;
        _client  = _factory.CreateClient();
    }
}