namespace IntegrationTest.WebApi;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Core.Contracts;

using FluentAssertions;

using global::WebApi.Controllers;

using IntegrationTest.WebApi.Fixtures;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Persistence;

using Xunit;

public class ShippingCompanyControllerTests : IntegrationTest
{
    public ShippingCompanyControllerTests(ApiWebApplicationFactory fixture)
        : base(fixture)
    {
    }

    #region get

    protected HttpClient GetClient()
    {
        return _factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(
                    services =>
                    {
                        services
                            .AddDbContext<ApplicationDbContext>(options =>
                                options.UseSqlServer(ConnectionString));
                    });
            })
            .CreateClient();
    }

    [Fact]
    public async Task GetAllTest()
    {
        var client = GetClient();

        var response = await client.GetAsync("/ShippingCompany");
        response.EnsureSuccessStatusCode();

        var dtos = await response.Content.ReadAsAsync<IList<ShippingCompanyController.ShippingCompanyDto>>();

        dtos.Should().HaveCount(150);

        dtos.Single(s => s.Id == 1).Should().BeEquivalentTo(
            new
            {
                Id   = 1,
                Name = "Atomflot",
            });
    }


    [Fact]
    public async Task GetTest()
    {
        var client = GetClient();

        var response = await client.GetAsync("/ShippingCompany/1");
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadAsAsync<ShippingCompanyController.ShippingCompanyDto>();

        dto.Should().BeEquivalentTo(
            new
            {
                Id   = 1,
                Name = "Atomflot",
            });
    }

    [Fact]
    public async Task GetFailTest()
    {
        var client = GetClient();

        var response = await client.GetAsync("/ShippingCompany/232");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion
}