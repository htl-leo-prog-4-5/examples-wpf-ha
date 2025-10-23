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

using System.IO;
using System.Linq;
using System.Reflection;
using EnterpriseApp.Repository.Abstraction.Entities;
using Framework.Tools.Tools;

namespace EnterpriseApp.Repository.Context
{
    public class EnterpriseAppDefaultData
    {
        private string DefaultDataDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public void CNCSeed(EnterpriseAppContext context, bool isTest)
        {
            var users = UserSeed(context);
            ConfigurationSeed(context, isTest);

            foreach (var user in users)
            {
                user.UserId = 0;
            }
            context.Set<User>().AddRange(users);
        }

        private User[] UserSeed(EnterpriseAppContext context)
        {
            var userImport = new CsvImport<User>();
            var users      = userImport.Read(DefaultDataDir + @"\DefaultData\User.csv").ToArray();
            return users;
        }

        private void ConfigurationSeed(EnterpriseAppContext context, bool isTest)
        {
            if (isTest)
            {
                var configurationImport = new CsvImport<Configuration>();
                var configurations      = configurationImport.Read(DefaultDataDir + @"\DefaultData\Configuration.csv").ToArray();

                context.Set<Configuration>().AddRange(configurations);
            }
        }
    }
}