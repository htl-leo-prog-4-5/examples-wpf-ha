

using System.Data.Entity;

namespace Survey.Repository.Context
{
    public class SurveyInitializerTest : DropCreateDatabaseAlways<SurveyContext>
    {
        protected override void Seed(SurveyContext context)
        {
            SurveyDefaultData.SurveySeed(context);
        }
    }
}
