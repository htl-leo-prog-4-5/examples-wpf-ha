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
using System.Linq;
using System.Reflection;
using EnterpriseApp.Repository.Context;
using Framework.Repository;
using Framework.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Repository
{
    public static class LiveDependencyRegisterExtensions
    {
        public static IServiceCollection RegisterTypesIncludingInternals(this IServiceCollection container, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
                {
                    string interfaceName = "I" + type.Name;
                    var interfaceType = type.GetInterface(interfaceName);
                    if (interfaceType != null)
                    {
                        container.AddTransient(interfaceType, type);
                    }
                }
            }

            return container;
        }

        public static IServiceCollection RegisterRepository(this IServiceCollection container, Action<DbContextOptionsBuilder> optionsAction)
        {
            var options = new DbContextOptionsBuilder<EnterpriseAppContext>();
            optionsAction(options);

            container.AddSingleton<DbContextOptions<EnterpriseAppContext>>(options.Options);
            container.AddScoped<EnterpriseAppContext, EnterpriseAppContext>();
            container.AddScoped<IUnitOfWork, UnitOfWork<EnterpriseAppContext>>();

            container.RegisterTypesIncludingInternals(typeof(Repository.UserRepository).Assembly);
            return container;
        }
    }
}