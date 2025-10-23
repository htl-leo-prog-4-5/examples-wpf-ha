using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.Logic.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise.WebServer.Controllers
{
    [Route("api/[controller]")]
    public class MyController 
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

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
