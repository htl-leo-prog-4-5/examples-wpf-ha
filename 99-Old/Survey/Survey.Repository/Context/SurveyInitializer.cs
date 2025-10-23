using System.Data.Entity;
using Survey.Repository.Entities;

namespace Survey.Repository.Context
{
	public class SurveyInitializer : MigrateDatabaseToLatestVersion<SurveyContext, Survey.Repository.Migrations.Configuration>
	{
		public override void InitializeDatabase(SurveyContext context)
		{
			base.InitializeDatabase(context);

			// add Testdata to database if no data are stored

			if (context.Poll.CountAsync().ConfigureAwait(false).GetAwaiter().GetResult() == 0)
			{
				SurveyDefaultData.SurveySeed(context);
				context.SaveChanges();
			}
		}
	}
}
