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
using System.Reflection;
using AutoMapper;
using EnterpriseApp.Logic;
using EnterpriseApp.Repository;
using EnterpriseApp.Repository.Context;
using EnterpriseApp.Shared;
using EnterpriseApp.WebAPI;
using EnterpriseApp.WebAPI.Controllers;
using Framework.WebAPI.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NLog;
using Swashbuckle.AspNetCore.Swagger;

namespace EnterpriseApp.Host
{
    public class Startup
    {
        private readonly string apiVersion = "1";
        private readonly string serviceName = "EnterpriseApp";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string sqlConnectString = Configuration.GetConnectionString("myDB");

            GlobalDiagnosticsContext.Set("connectionString", sqlConnectString);
            GlobalDiagnosticsContext.Set("version",          Assembly.GetExecutingAssembly().GetName().Version.ToString());
            GlobalDiagnosticsContext.Set("application",      "EnterpriseApp.WebAPI.Server");
            GlobalDiagnosticsContext.Set("username",         Environment.UserName);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var controllerAssembly = typeof(UserController).Assembly;

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

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
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddApplicationPart(controllerAssembly);

            services.AddAuthorization();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5005";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "EnterpriseAppAPI";
                });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = $"{serviceName} API", Version = $"v{apiVersion}" });
                    c.DescribeAllEnumsAsStrings();
                    c.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}\\{controllerAssembly.GetName().Name}.XML");
                });


            services.RegisterRepository(options =>
                SqlServerDbContextOptionsExtensions.UseSqlServer(options, Configuration.GetConnectionString("myDB")));
            services.RegisterLogic();

            services.AddScoped<IEnterpriseAppUserContext, EnterpriseAppUserContext>();

            services.RegisterMapper(new MapperConfiguration(cfg => { cfg.AddProfile<LogicAutoMapperProfile>(); }));

            var dbcontext = services.BuildServiceProvider().GetService<EnterpriseAppContext>();
            dbcontext.InitializeDatabase(false, false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Open Database here

            EnterpriseAppContext.InitializeDatabase2(false, false);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(
                c =>
                {
                    var version = $"v{apiVersion}";
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{serviceName} {version}");
                });

            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}