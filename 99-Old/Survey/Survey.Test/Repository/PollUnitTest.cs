using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Survey.Repository;

namespace Survey.Test.Repository
{
	[TestClass]
	public class PollUnitTest
	{
		[TestMethod]
		public async Task TestMethod1()
		{
			var rep = new PollRepository();
			var m = await rep.GetPolls();

			Assert.IsNotNull(m);
		}
	}
}
