using System;
using System.Linq;
using EnterpriseSimpleV2.Repository.Abstraction.Entities;
using EnterpriseSimpleV2.Repository.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace EnterpriseSimpleV2.Repository.Context
{
    public class MyContext : DbContext
    {
        public static Action<DbContextOptionsBuilder> OnConfigure;

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            OnConfigure?.Invoke(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTable>().Map();
        }

        #region Migration

        public static string ConnectString { get; set; } =
            @"Data Source = (LocalDB)\\MSSQLLocalDB; Initial Catalog = MyEnterpriseSample; Integrated Security = True";

        private static DbContextOptions<MyContext> GetDbOptions()
        {
            var options = new DbContextOptionsBuilder<MyContext>();
            options.UseSqlServer(ConnectString);
            return options.Options;
        }

        public MyContext() : base(GetDbOptions())
        {
        }

        #endregion
    }
}