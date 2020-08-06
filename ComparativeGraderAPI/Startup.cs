using System;
using System.Collections.Generic;
using FluentValidation.AspNetCore;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using ComparativeGraderAPI.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ComparativeGraderAPI.Application.Semesters;
using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Application.ServiceLayers.DataAccess;
using ComparativeGraderAPI.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ComparativeGraderAPI.Security.Security_Interfaces;
using ComparativeGraderAPI.Security;
//using Microsoft.EntityFrameworkCore.SqlServer;

namespace ComparativeGraderAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GradingDataContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            //services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddMediatR(typeof(Startup));

            services.AddControllers().AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<List>();
                
            });

            //services.AddIdentityCore<ProfessorUser>()
            //    .AddEntityFrameworkStores<GradingDataContext>()
            //    .AddSignInManager<SignInManager<ProfessorUser>>();
            var builder = services.AddIdentityCore<ProfessorUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<GradingDataContext>();
            identityBuilder.AddSignInManager<SignInManager<ProfessorUser>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Super seceret key"));//change this to a user secret.

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<ISemestersAccess, SemestersAccess>();
            services.AddScoped<ICourseAccess, CoursesAccess>();
            services.AddScoped<IAssignmentAccess, AssignmentAccess>();
            services.AddScoped<ISubmissionsAccess, SubmissionsAccess>();
            services.AddScoped<ICourseVerifier, CourseVerifier>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
