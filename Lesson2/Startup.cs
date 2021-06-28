using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Data;
using Shared;
using Shared.Repository;
using Shared.Models;
using Shared.Services.Migration;
using Microsoft.EntityFrameworkCore;
using Shared.DBContext;
using Lesson2.Validation;
using Lesson2.Service;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shared.Services.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Lesson2.Schedulers;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using Lesson2.Autorization;

namespace Lesson2
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
            var connectionString = Configuration.GetConnectionString("MSSQLDataBase");

            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(nameof(Shared))));

            services.AddIdentity<User, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuthOptions:SecretKey"])),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddAutoMapper(typeof(MappingProfile));


            services.AddTransient<ITankRepository, TankRepository>();
            services.AddTransient<IFactoryRepository, FactoryRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IJwtService, JwtService>();

            services.AddSingleton<TankValidator>();
            services.AddSingleton<FactoryValidator>();
            services.AddSingleton<UnitValidator>();
            services.AddSingleton<UserValidator>();

            services.AddHostedService<UpdateVolumeService>();



            services.AddControllers(c =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                c.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthorization(options =>  
            {
                options.AddPolicy("Administrator",
                    policy => policy.RequireRole("Admin"));
                options.AddPolicy("GeneralRight",
                    policy => policy.RequireRole("Admin", "Manager", "User"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lesson2", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                    "JWT Authorization header using the Bearer scheme." + Environment.NewLine +
                    "Enter your token in the text input below.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                });
                c.OperationFilter<AuthOperationFilter>();

            });

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            
            services.AddSingleton<RemindersJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RemindersJob),
                cronExpression: "0 * * ? * *"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lesson2 v1"));
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
