﻿

using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Survey.Repository
{
    public class PollRepository : SurveyRepository
	{
		#region Get

		public async Task<Entities.Poll[]> GetPolls()
		{
            return await Context.Poll.
                Include((d) => d.PollAnswers).
				Include((d) => d.PollVotes).
				ToArrayAsync();
		}
		public async Task<Entities.Poll> GetPoll(int pollid)
		{
			return await Context.Poll.
				Where((p) => p.PollID == pollid).
				Include((d) => d.PollAnswers).
				Include((d) => d.PollVotes).
				SingleOrDefaultAsync();
		}
		public async Task<int> GetMaxPollID()
		{
			return Context.Poll.Max((p) => p.PollID);
		}

		#endregion

		#region update

		public async Task<int> MySaveChangesAsync()
		{
			return await Context.SaveChangesAsync();
		}

		public void AddVote(Entities.PollVote vote)
		{
			Context.PollVote.Add(vote);
		}

		public async Task AddNewPoll(Entities.Poll poll)
		{
			Context.Poll.Add(poll);
            foreach (var pa in poll.PollAnswers)
            {
               Context.PollAnswer.Add(pa);
            }
			await Context.SaveChangesAsync();
		}

		#endregion

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~MachineRepository() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
