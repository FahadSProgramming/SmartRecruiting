using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartRecruiting.Application;
using SmartRecruiting.Application.Interfaces;
using SmartRecruiting.API.Infrastructure;
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
            services.Configure<Application.Common.TokenParameters>(Configuration.GetSection("Authentication:TokenConfiguration"));
            services.AddOptions();
            services.AddScoped<IPasswordGenerationService, PasswordGenerationService>();
            services.AddScoped<IJwtTokenGenerationService, JwtTokenGenerationService>();

            // Add Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration.GetSection("Authentication:TokenConfiguration:SecurityKey").Value.ToString())),
                    ValidateAudience = false,
                    ValidateIssuer = false
                    };
                });

            services.AddControllers(options => {
                    options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ISmartRecruitingDbContext>());;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseExceptionHandling();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}