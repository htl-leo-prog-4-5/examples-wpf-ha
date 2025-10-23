namespace UnitTest.WebApi;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Base.Tools.CsvImport;

using Core.Contracts;
using Core.Entities;

using FluentAssertions;

using global::WebApi.Controllers;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using UnitTest.WebApi.Fixtures;

using Xunit;

public class ShippingCompanyControllerTests : IntegrationTest
{
    public ShippingCompanyControllerTests(ApiWebApplicationFactory fixture)
        : base(fixture)
    {
    }

    protected IUnitOfWork GetUnitOfWork(bool empty)
    {
        var uow                       = Substitute.For<IUnitOfWork>();
        var shippingCompanyRepository = Substitute.For<IShippingCompanyRepository>();

        var companies = new CsvImport<ShippingCompany>().Read(
            """
                Id;Name;City;PLZ;Street;StreetNo
                1;Atomflot;City;PLZ;Street;20
                2;Royal Olympic Cruises;;;;
                3;V.Ships, Marina Cruises, Tropicana Cruises, B&BS;;;;
                4;Royal Caribbean International;;;;
                """
                .Replace("\r", "").Split('\n'));

        if (!empty)
        {
            shippingCompanyRepository.GetNoTrackingAsync(
                    Arg.Any<Expression<Func<ShippingCompany, bool>>>(),
                    Arg.Any<Func<IQueryable<ShippingCompany>, IOrderedQueryable<ShippingCompany>>>(),
                    Arg.Any<string[]>())
                .Returns(companies);

            shippingCompanyRepository.GetByIdAsync(1, Arg.Any<string[]>())
                .Returns(companies.Single(s => s.Id == 1));

            uow.ShippingCompanyRepository.Returns(shippingCompanyRepository);
        }

        return uow;
    }

    #region get

    protected HttpClient GetClient(bool empty)
    {
        return _factory
            .WithWebHostBuilder(builder => { builder.ConfigureServices(services => { services.AddSingleton<IUnitOfWork>(GetUnitOfWork(empty)); }); })
            .CreateClient();
    }

    [Fact]
    public async Task GetAllTest()
    {
        var client = GetClient(false);

        var response = await client.GetAsync("/ShippingCompany");
        response.EnsureSuccessStatusCode();

        var dtos = await response.Content.ReadAsAsync<IList<ShippingCompanyController.ShippingCompanyDto>>();

        dtos.Should().HaveCount(4);

        dtos.Single(s => s.Id == 1).Should().BeEquivalentTo(
            new
            {
                Id       = 1,
                Name     = "Atomflot",
                Street   = "Street",
                City     = "City",
                Plz      = "PLZ",
                StreetNo = "20",
            });
    }

    [Fact]
    public async Task GetAllEmptyTest()
    {
        var client = GetClient(true);

        var response = await client.GetAsync("/ShippingCompany");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTest()
    {
        var client = GetClient(false);

        var response = await client.GetAsync("/ShippingCompany/1");
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadAsAsync<ShippingCompanyController.ShippingCompanyDto>();

        dto.Should().BeEquivalentTo(
            new
            {
                Id       = 1,
                Name     = "Atomflot",
                Street   = "Street",
                City     = "City",
                Plz      = "PLZ",
                StreetNo = "20",
            });
    }

    [Fact]
    public async Task GetFailTest()
    {
        var client = GetClient(false);

        var response = await client.GetAsync("/ShippingCompany/232");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion
}