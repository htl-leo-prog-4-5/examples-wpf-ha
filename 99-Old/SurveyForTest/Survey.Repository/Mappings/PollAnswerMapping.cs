
using Survey.Repository.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Survey.Repository.Mappings
{
	public class PollAnswerMapping : EntityTypeConfiguration<PollAnswer>
    {
		public PollAnswerMapping()
        {
            HasKey(m => m.PollAnswerID);

            Property((m) => m.Answer).
                IsRequired().
                HasMaxLength(64);

			HasRequired(x => x.Poll).
				WithMany(t => t.PollAnswers).
				Map(m => m.MapKey("PollID"));
		}
	}
}
