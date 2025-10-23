using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Survey.DTO;

namespace Survey.ServiceProxy
{
    public class ServiceProxy
    {
		protected readonly string _webserverurl = ConfigurationManager.AppSettings["SurveyWebApi"] ?? @"http://localhost:3752/";

		protected HttpClient CreateHttpClient()
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri(_webserverurl);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		protected readonly string _api = @"api/Poll";

		public async Task<Poll> Get(int id)
		{
			using (HttpClient client = CreateHttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(_api + "/" + id);
				if (response.IsSuccessStatusCode)
				{
					Poll value = await response.Content.ReadAsAsync<Poll>();

					return value;
				}
			}
			return null;
		}

		public async Task<Poll> GetCurrent()
		{
			var all = await GetAll();
			return all.OrderByDescending((p)=>p.PollID).FirstOrDefault();
		}

		public async Task<IEnumerable<Poll>> GetAll()
		{
			using (HttpClient client = CreateHttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(_api);
				if (response.IsSuccessStatusCode)
				{
					IEnumerable<Poll> values = await response.Content.ReadAsAsync<IEnumerable<Poll>>();
					return values;
				}
				return null;
			}
		}

		public async Task<int> GetCurrentPollID()
		{
			using (HttpClient client = CreateHttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(_api +"?a=1&b=1");
				if (response.IsSuccessStatusCode)
				{
					var values = await response.Content.ReadAsAsync<int>();
					return values;
				}
				return -1;
			}
		}

		public async Task<IEnumerable<PollResult>> GetResults()
		{
			using (HttpClient client = CreateHttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(_api + "?a=1");
				if (response.IsSuccessStatusCode)
				{
					var values = await response.Content.ReadAsAsync<IEnumerable<PollResult>>();
					return values;
				}
				return null;
			}
		}
		public async Task<IEnumerable<PollResultSummary>> GetResultsSummary()
		{
			using (HttpClient client = CreateHttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(_api + "?a=1&b=1&c=1");
				if (response.IsSuccessStatusCode)
				{
					var values = await response.Content.ReadAsAsync<IEnumerable<PollResultSummary>>();
					return values;
				}
				return null;
			}
		}


		public async Task SetAnswer(int id, int answerid, string username)
		{
			using (HttpClient client = CreateHttpClient())
			{
				HttpResponseMessage response = await client.PutAsJsonAsync($"{_api}/{id}?answerid={answerid}&username={username}", "dummy");

				if (response.IsSuccessStatusCode)
				{
					return;
				}
				return;
			}
		}
		public async Task AddNewSurvey(Poll poll)
		{
			using (HttpClient client = CreateHttpClient())
			{
				HttpResponseMessage response = await client.PostAsJsonAsync<Poll>(_api,poll);

				if (response.IsSuccessStatusCode)
				{
					return;
				}
				return;
			}
		}
	}
}
