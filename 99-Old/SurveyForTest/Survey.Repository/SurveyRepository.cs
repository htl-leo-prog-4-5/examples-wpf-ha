
using Survey.Repository.Context;


namespace Survey.Repository
{
	public class SurveyRepository
	{
		private SurveyContext _context = new SurveyContext();
		public SurveyContext Context { get { return _context; } }
    }
}
