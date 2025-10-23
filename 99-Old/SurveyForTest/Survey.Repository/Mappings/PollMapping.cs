
using Survey.Repository.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Survey.Repository.Mappings
{
	public class PollMapping : EntityTypeConfiguration<Poll>
    {
		public PollMapping()
        {
            HasKey(m => m.PollID);

            Property((m) => m.Name).
                IsRequired().
                HasMaxLength(64);
		}
	}
}
