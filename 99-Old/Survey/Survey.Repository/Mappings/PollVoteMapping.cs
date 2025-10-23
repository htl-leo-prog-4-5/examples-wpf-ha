
using Survey.Repository.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Survey.Repository.Mappings
{
	public class PollVoteMapping : EntityTypeConfiguration<PollVote>
    {
		public PollVoteMapping()
        {
            HasKey(m => m.PollVoteID);

			Property((m) => m.UserName).
				IsRequired().
				HasMaxLength(64);

			HasRequired(a => a.PollAnswer).
				WithMany().WillCascadeOnDelete(false);

			HasRequired(x => x.Poll).
				WithMany(t => t.PollVotes).
				Map(m => m.MapKey("PollID"));

		}
	}
}
