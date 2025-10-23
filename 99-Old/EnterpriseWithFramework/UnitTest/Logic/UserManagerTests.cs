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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseApp.Repository.Abstraction.Entities;
using EnterpriseApp.Repository.Abstraction;
using EnterpriseApp.UnitTest.Logic;
using EnterpriseApp.Logic.Abstraction;
using EnterpriseApp.Logic.Manager;
using FluentAssertions;

using Framework.Repository.Abstraction;

using NSubstitute;

using Xunit;

namespace EnterpriseApp.UnitTest.Logic
{
    public class UserManagerTests : LogicTests
    {


        [Fact]
        public async Task GetUserNone()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var rep        = Substitute.For<IUserRepository>();

            var ctrl = new UserManager(unitOfWork, rep, Mapper);

            var machineEntity = new User[0];
            rep.GetAll().Returns(machineEntity);

            var users = (await ctrl.GetAll()).ToArray();
            users.Length.Should().Be(0);
        }

        [Fact]
        public async Task GetMachinesOne()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var rep        = Substitute.For<IUserRepository>();
            var repC       = Substitute.For<IConfigurationRepository>();

            var ctrl       = new UserManager(unitOfWork, rep, Mapper);

            var machineEntity = new[]
            {
                new User
                {
                    UserId              = 1,
                    UserPassword        = "Maxi",
                    UserName            = "Mini",
                }
            };
            rep.GetAll().Returns(machineEntity);

            var machines = (await ctrl.GetAll()).ToArray();
            machines.Length.Should().Be(1);
            machines[0].UserId.Should().Be(1);
            machines[0].UserPassword.Should().Be("Maxi");
        }
    }
}