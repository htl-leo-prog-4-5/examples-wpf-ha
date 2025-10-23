using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.DTO;
using Enterprise.Logic.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise.WebServer.Controllers
{
    [Route("api/[controller]")]
    public class MyController : Controller
    {
        private IMyLogic _logic;

        public MyController(IMyLogic logic)
        {
            _logic = logic;
        }

        // GET api/values
        [HttpGet]
        public  int Get()
        {
            return _logic.GetZero();
        }

        [HttpGet("Info")]
        public async Task<IActionResult> GetInfo()
        {
            var ret = await _logic.GetInfo();
            return Ok(ret);
        }
    }
}
