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
        var context          = TestFixture.CreateDbContext();
        var ticketRepository = new TicketRepository(context);
        var gameRepository   = new GameRepository(context);
        var tipRepository    = new TipRepository(context);
        var officeRepository = new OfficeRepository(context);
        var stateRepository  = new StateRepository(context);
        var uow              = new UnitOfWork(context, tipRepository, officeRepository, ticketRepository, gameRepository, stateRepository);
        return uow;
    }
}