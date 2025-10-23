using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Survey.DTO;
using Survey.WebAPI;
using Survey.WebAPI.Controllers;

namespace Survey.WebAPI.Tests.Controllers
{
	[TestClass]
	public class PollControllerTest
	{
		[TestMethod]
		public async Task Get()
		{
			// Arrange
			PollController controller = new PollController();

			// Act
			IEnumerable<Poll> result = await controller.Get();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual("value1", result.ElementAt(0));
			Assert.AreEqual("value2", result.ElementAt(1));
		}

		[TestMethod]
		public async Task GetById()
		{
			// Arrange
			PollController controller = new PollController();

			// Act
			var result = await controller.Get(5);

			// Assert
			Assert.AreEqual("value", result);
		}

		[TestMethod]
		public void Post()
		{
			// Arrange
			PollController controller = new PollController();

			// Act
			controller.Post(new Poll());

			// Assert
		}

		[TestMethod]
		public void Put()
		{
			// Arrange
			PollController controller = new PollController();

			// Act
			//controller.Put(5, "value");

			// Assert
		}

		[TestMethod]
		public void Delete()
		{
			// Arrange
			PollController controller = new PollController();

			// Act
			controller.Delete(5);

			// Assert
		}
	}
}
