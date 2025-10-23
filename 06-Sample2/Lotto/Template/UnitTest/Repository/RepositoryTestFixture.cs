using System.Threading.Tasks;

using Base.Tools.CsvImport;

using Core.Entities;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Xunit;

namespace UnitTest.Repository;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Base.Core.Contracts;

public class RepositoryTestFixture : RepositoryTestFixtureBase<ApplicationDbContext>, IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        //drop and recreate the test Db every time. 

        await InitializeDatabase();
    }

    public async Task DisposeAsync()
    {
        await Task.CompletedTask;
    }

    private async Task InitializeDatabase()
    {
        using (var dbContext = CreateDbContext())
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.MigrateAsync();

            await ImportDataAsync(dbContext);

            await dbContext.SaveChangesAsync();
        }
    }


    private async Task ImportDataAsync(ApplicationDbContext dbContext)
    {
        var offices = (await new CsvImport<Office>().ReadAsync("Repository/TestData/Office.csv")).ToDictionary(s => s.Id);
        var games   = (await new CsvImport<Game>().ReadAsync("Repository/TestData/Game.csv")).ToDictionary(l => l.Id);
        var tickets = (await new CsvImport<Ticket>().ReadAsync("Repository/TestData/Ticket.csv")).ToDictionary(w => w.Id);
        var tips    = (await new CsvImport<Tip>().ReadAsync("Repository/TestData/Tip.csv")).ToDictionary(d => d.Id);


        foreach (var ticket in tickets.Values)
        {
            ticket.Office   = offices[ticket.OfficeId];
            ticket.OfficeId = 0;

            ticket.Game   = games[ticket.GameId];
            ticket.GameId = 0;
        }

        foreach (var tip in tips.Values)
        {
            tip.Ticket   = tickets[tip.TicketId];
            tip.TicketId = 0;
        }

        void SetIdNull<T>(IList<T> list) where T : IEntityObject
        {
            foreach (var item in list)
            {
                item.Id = 0;
            }
        }

        SetIdNull(games.Values.ToList());
        SetIdNull(offices.Values.ToList());
        SetIdNull(tickets.Values.ToList());
        SetIdNull(tips.Values.ToList());

        await dbContext.Set<Game>().AddRangeAsync(games.Values.ToList());
        await dbContext.Set<Tip>().AddRangeAsync(tips.Values.ToList());
        await dbContext.Set<Office>().AddRangeAsync(offices.Values.ToList());
        await dbContext.Set<Ticket>().AddRangeAsync(tickets.Values.ToList());
    }

    public override ApplicationDbContext CreateDbContext()
    {
        var connectString = @"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = Lotto_Test; Integrated Security = True";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer(connectString, x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}