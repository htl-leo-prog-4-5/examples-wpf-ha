using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Survey.Repository;

namespace Survey.Logic.Test
{
	[TestClass]
	public class PollControllerUnitTest
	{
		[TestMethod]
		public async Task TestMethod1()
		{
			var ctrl = new SurveyController();
			var m = await ctrl.GetAll();

			Assert.IsNotNull(m);
		}
	}
}
