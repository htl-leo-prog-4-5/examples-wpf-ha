/*
  This file is part of CNCLib - A library for stepper motors.

  Copyright (c) Herbert Aitenbichler

  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
  and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnterpriseApp.Logic.Abstraction;
using EnterpriseApp.Logic.Abstraction.DTO;
using EnterpriseApp.Shared;
using Framework.WebAPI.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseApp.WebAPI.Controllers
{
    /// <summary>
    /// User Controller.
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager    _manager;
        private readonly IEnterpriseAppUserContext _userContext;

        public UserController(IUserManager manager, IEnterpriseAppUserContext userContext)
        {
            _manager     = manager ?? throw new ArgumentNullException();
            _userContext = userContext ?? throw new ArgumentNullException();
            ((EnterpriseAppUserContext)_userContext).InitFromController(this);
        }

        #region default REST

        /// <summary>
        /// Get all available users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await this.GetAll(_manager);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            return await this.Get(_manager, id);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add([FromBody] User value)
        {
            return await this.Add<User, int>(_manager, value);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] User value)
        {
            return await this.Update<User, int>(_manager, id, value.UserId, value);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await this.Delete<User, int>(_manager, id);
        }

        #endregion

        #region bulk

        [HttpPost("bulk")]
        public async Task<ActionResult<IEnumerable<UriAndValue<User>>>> Add([FromBody] IEnumerable<User> values)
        {
            return await this.Add<User, int>(_manager, values);
        }

        [HttpPut("bulk")]
        public async Task<ActionResult> Update([FromBody] IEnumerable<User> values)
        {
            return await this.Update<User, int>(_manager, values);
        }

        [HttpDelete("bulk")]
        public async Task<ActionResult> Delete(int[] ids)
        {
            return await this.Delete<User, int>(_manager, ids);
        }

        #endregion
    }
}