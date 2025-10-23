using Persistence;

using Xunit;

namespace UnitTest.Repository;

using Base.UnitTest.Repository;

using Core.Contracts;

[CollectionDefinition(nameof(MyRepositoryTests))]
public class MyRepositoryTests : ICollectionFixture<RepositoryTestFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection(nameof(MyRepositoryTests))]
public abstract class RepositoryTest : RepositoryTestBase<ApplicationDbContext>
{
    protected RepositoryTest(RepositoryTestFixture testFixture) : base(testFixture)
    {
    }

    protected IUnitOfWork GetUnitOfWork()
    {
        var context             = TestFixture.CreateDbContext();
        var planeRepository     = new PlaneRepository(context);
        var routeRepository     = new RouteRepository(context);
        var hotelRepository     = new HotelRepository(context);
        var routeStepRepository = new RouteStepRepository(context);
        var tripRepository      = new TripRepository(context);
        var shipRepository      = new ShipRepository(context);
        var uow = new UnitOfWork(context,
            tripRepository,
            routeRepository,
            routeStepRepository,
            hotelRepository,
            shipRepository,
            planeRepository
        );
        return uow;
    }
}