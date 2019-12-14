using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartRecruiting.Application;
using SmartRecruiting.Application.Interfaces;
using SmartRecruiting.Persistence;
using SmartRecruiting.Services.AuthenticationServices;

namespace SmartRecruiting.API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // Add MediatR
            services.AddMediatR(typeof(SmartRecruiting.Application.Users.CreateUserCommand).GetTypeInfo().Assembly);

            // Add AutoMapper
            services.AddAutoMapper(typeof(SmartRecruiting.Application.Infrastructure.MappingProfile).GetTypeInfo().Assembly);

            // Add DbContext
            services.AddDbContext<ISmartRecruitingDbContext, SmartRecruitingDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("ConnectionString")));

            // Add services
            services.AddScoped<IPasswordGenerationService, PasswordGenerationService>();
            services.AddScoped<IJwtTokenGenerationService, JwtTokenGenerationService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}