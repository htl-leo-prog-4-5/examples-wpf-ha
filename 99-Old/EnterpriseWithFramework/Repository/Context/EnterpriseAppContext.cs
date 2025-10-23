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

using System.Linq;
using EnterpriseApp.Repository.Abstraction.Entities;
using EnterpriseApp.Repository.Mappings;
using Framework.Repository.Abstraction.Entities;
using Framework.Repository.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EnterpriseApp.Repository.Context
{
    public class EnterpriseAppContext : DbContext
    {
        public EnterpriseAppContext(DbContextOptions<EnterpriseAppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration -------------------------------------

            modelBuilder.Entity<Configuration>().Map();

            // User -------------------------------------

            modelBuilder.Entity<User>().Map();

            // -------------------------------------

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // -------------------------------------

            modelBuilder.Entity<Log>().Map();

            base.OnModelCreating(modelBuilder);
        }

        protected void InitOrUpdateDatabase(bool isTest)
        {
            if (Set<User>().Any())
            {
                ModifyWrongData();
                SaveChanges();
            }
            else
            {
                new EnterpriseAppDefaultData().CNCSeed(this, isTest);
                SaveChanges();
            }
        }

        private void ModifyWrongData()
        {
        }

        public static void InitializeDatabase2(bool dropDatabase, bool isTest)
        {
            //            using (var ctx = Dependency.Container.Resolve<EnterpriseAppContext>())
            //            {
            //                ctx.InitializeDatabase(dropDatabase, isTest);
            //            }
        }

        public void InitializeDatabase(bool dropDatabase, bool isTest)
        {
            if (dropDatabase)
            {
                Database.EnsureDeleted();
            }

            Database.Migrate();

            InitOrUpdateDatabase(isTest);
        }
    }
}