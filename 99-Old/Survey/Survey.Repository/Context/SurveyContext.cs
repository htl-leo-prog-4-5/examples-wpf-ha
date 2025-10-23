using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using Survey.Repository.Entities;
using Survey.Repository.Mappings;

namespace Survey.Repository.Context
{
	public class SurveyContext : DbContext
    {
        public SurveyContext()
        {
            Configure();
        }

        public SurveyContext(string connectionString):base(connectionString)
        {
            Configure();    
        }
        public DbSet<Poll> Poll { get; set; }
		public DbSet<PollAnswer> PollAnswer { get; set; }
		public DbSet<PollVote> PollVote { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PollMapping());
			modelBuilder.Configurations.Add(new PollAnswerMapping());
			modelBuilder.Configurations.Add(new PollVoteMapping());

			// -------------------------------------

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // -------------------------------------

            base.OnModelCreating(modelBuilder);

        }

        private void Configure()
        {
            System.Data.Entity.Database.SetInitializer<SurveyContext>(new SurveyInitializer());
			//Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ValidateOnSaveEnabled = true; 
        }
    }
}
