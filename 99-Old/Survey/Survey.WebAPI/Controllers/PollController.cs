using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Survey.DTO;
using Survey.Logic;

namespace Survey.WebAPI.Controllers
{
	public class PollController : ApiController
	{
		// GET api/values
		public async Task<IEnumerable<Poll>> Get()
		{
			var ctrl = new SurveyController();
			return await ctrl.GetAll();
		}

		public async Task<IEnumerable<PollResult>> GetResult(int a)
		{
			var ctrl = new SurveyController();
			return await ctrl.GetResult(await ctrl.GetCurrentPollID());
		}

		public async Task<IEnumerable<PollResultSummary>> GetResultSummary(int a,int b, int c)
		{
			var ctrl = new SurveyController();
			return await ctrl.GetResultSummary(await ctrl.GetCurrentPollID());
		}

		public async Task<int> GetCurrentPollID(int a, int b)
		{
			var ctrl = new SurveyController();
			return await ctrl.GetCurrentPollID();
		}

		// GET api/values/5
		public async Task<Poll> Get(int id)
		{
			var ctrl = new SurveyController();
			return (await ctrl.GetAll()).FirstOrDefault((p) => p.PollID == id);
		}

		// POST api/values
		public async void Post([FromBody]Poll value)
		{
			var ctrl = new SurveyController();
			bool ret = await ctrl.AddPoll(value);
		}

		// PUT api/values/
		public async void Put(int id, int answerid, string username)
		{
			var ctrl = new SurveyController();
			bool ret = await ctrl.Answer(id, answerid, username);
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
