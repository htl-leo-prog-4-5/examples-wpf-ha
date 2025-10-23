using AutoMapper;
using EnterpriseSimpleV2.Logic;
using EnterpriseSimpleV2.Logic.Abstraction;
using EnterpriseSimpleV2.Logic.Manager;
using EnterpriseSimpleV2.Repository.Context;
using EnterpriseSimpleV2.WebAPI.Controllers;
using EnterpriseSimpleV2.WebAPI.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NLog;
using Swashbuckle.AspNetCore.Swagger;

namespace EnterpriseSimpleV2.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var controllerAssembly = typeof(AddInt16Controller).Assembly;

            services.AddDbContext<MyContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyDatabase")));

            services.AddCors(options =>
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddTransient<UnhandledExceptionFilter>();
            services.AddTransient<ValidateRequestDataFilter>();
            services.AddTransient<MethodCallLogFilter>();
            services.AddMvc(
                    options =>
                    {
                        options.Filters.AddService<ValidateRequestDataFilter>();
                        options.Filters.AddService<UnhandledExceptionFilter>();
                        options.Filters.AddService<MethodCallLogFilter>();
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddApplicationPart(controllerAssembly);

            services.AddTransient<IAddInt16Manager, AddInt16Manager>();
            services.AddTransient<IMyTableManager, MyTableManager>();
            services.AddTransient<IMyTableRepository, MyTableRepository>();

            services.AddScoped<MyContext, MyContext>();

            services.AddSingleton<IMapper>(CreateMapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LogicAutoMapperProfile>();
            })));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "EnterpriseSimpleV2 API", Version = "v1"}); });
        }

        public static IMapper CreateMapper(MapperConfiguration mapperConfiguration)
        {
            mapperConfiguration.AssertConfigurationIsValid();

            IMapper mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Open Database here
            bool dropDatabase = false;

            var options = new DbContextOptionsBuilder<MyContext>();
            options.UseSqlServer(Configuration.GetConnectionString("MyDatabase"));

            using (var ctx = new MyContext(options.Options))
            {
                if (dropDatabase)
                {
                    ctx.Database.EnsureDeleted();
                }

                ctx.Database.Migrate();
            }

            GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("MyDatabase"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });

            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}