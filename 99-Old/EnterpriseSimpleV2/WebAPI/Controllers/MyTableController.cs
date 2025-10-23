using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnterpriseSimpleV2.Logic.Abstraction;
using EnterpriseSimpleV2.Logic.Abstraction.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseSimpleV2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class MyTableController : Controller
    {
        private IMyTableManager _manager;

        public MyTableController(IMyTableManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyTable>>> Get()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MyTable>> Get(int id)
        {
            return Ok(await _manager.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<MyTable>> Add([FromBody] MyTable value)
        {
            int newId = await _manager.Add(value);
            string newUri = GetCurrentUri() + "/" + newId;
            return Created(newUri, await _manager.Get(newId));
        }


        private string GetCurrentUri()
        {
            if (Request == null)
            {
                // unit test => no Request available 
                return "dummy";
            }

            return $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
        }
    }
}