
namespace Survey.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Survey.Repository.Context.SurveyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
			ContextKey = "Survey.Repository.Context.SurveyContext";
        }

        protected override void Seed(Survey.Repository.Context.SurveyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
