using Fastretro.API.Data;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Fastretro.API
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
            services.AddControllers();
            services.AddCors();
            services.AddScoped<DbContext, DataContext>();
            services.AddDbContext<DataContext>(db => db.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(Startup).Namespace)));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICurrentUsersInRetroBoardServices, CurrentUsersInRetroBoardServices>();
            services.AddScoped<IFreshCurrentUserInRetroBoardServices, FreshCurrentUserInRetroBoardServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.Authority = "https://securetoken.google.com/fastretro-64ade";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/fastretro-64ade",
                        ValidateAudience = true,
                        ValidAudience = "fastretro-64ade",
                        ValidateLifetime = true
                    };
                });
            services.AddOptions();

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("./Credentials/fastretro-64ade-firebase-adminsdk-jmaxe-4b745ba94c.json"),
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
