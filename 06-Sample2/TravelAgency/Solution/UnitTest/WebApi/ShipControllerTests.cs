namespace UnitTest.WebApi;

using System;
using System.Collections.Generic;
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

public class ShipControllerTests : IntegrationTest
{
    public ShipControllerTests(ApiWebApplicationFactory fixture)
        : base(fixture)
    {
    }

    protected IUnitOfWork GetUnitOfWork(bool empty)
    {
        var uow            = Substitute.For<IUnitOfWork>();
        var shipRepository = Substitute.For<IShipRepository>();

        var offices = new CsvImport<Ship>().Read(
            """
                Id;Name;Owner;PassengerCapacity;CargoCapacity;MaxSpeed
                1;Queen Mary 2;Cunard Line;2691;0;30
                2;Oasis of the Seas;Royal Caribbean International;6780;0;25
                3;Emma Maersk;AP Moller-Maersk Group;0;156907;43
                """
                .Replace("\r", "").Split('\n'));

        if (!empty)
        {
            shipRepository.GetNoTrackingAsync(
                    Arg.Any<Expression<Func<Ship, bool>>>(),
                    Arg.Any<Func<IQueryable<Ship>, IOrderedQueryable<Ship>>>(),
                    Arg.Any<string[]>())
                .Returns(offices);

            shipRepository.GetByIdAsync(1)
                .Returns(offices.Single(s => s.Id == 1));

            uow.ShipRepository.Returns(shipRepository);
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

        var response = await client.GetAsync("/ship");
        response.EnsureSuccessStatusCode();

        var dtos = await response.Content.ReadAsAsync<IList<ShipController.ShipDto>>();

        dtos.Should().HaveCount(4);

        dtos.Single(s => s.Id == 1).Should().BeEquivalentTo(
            new
            {
                Id      = 1,
                No      = "16906",
                Name    = "GASCHURN",
                Address = "VBG",
            });
    }

    [Fact]
    public async Task GetAllEmptyTest()
    {
        var client = GetClient(true);

        var response = await client.GetAsync("/ship");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTest()
    {
        var client = GetClient(false);

        var response = await client.GetAsync("/ship/1");
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadAsAsync<ShipController.ShipDto>();

        dto.Should().BeEquivalentTo(
            new
            {
                Id      = 1,
                No      = "16906",
                Name    = "GASCHURN",
                Address = "VBG",
            });
    }

    [Fact]
    public async Task GetFailTest()
    {
        var client = GetClient(false);

        var response = await client.GetAsync("/ship/232");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion
}