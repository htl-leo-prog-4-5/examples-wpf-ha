using System;
using System.Threading.Tasks;
using EnterpriseSimpleV2.Logic.Abstraction;
using EnterpriseSimpleV2.Logic.Abstraction.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseSimpleV2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AddInt16Controller : Controller
    {
        private IAddInt16Manager _manager;

        public AddInt16Controller(IAddInt16Manager manager)
        {
            _manager = manager ?? throw new ArgumentNullException();
        }

        [HttpGet("simple")]
        public async Task<ActionResult<int>> GetSimple(int a, int b)
        {
            return await _manager.AddSimple(a, b);
        }

        [HttpGet("simple3")]
        public async Task<ActionResult<int>> GetSimple3(int a, int b)
        {
            return await _manager.AddSimple(a, b);
        }

        [HttpGet]
        public async Task<ActionResult<Add16>> Get(int a, int b)
        {
            return await _manager.Add(a, b);
        }
    }
}