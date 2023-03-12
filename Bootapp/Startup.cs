using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootapp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bootapp.Util;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Bootapp.Service;
using Bootapp.Helpers;
using Bootapp.MiddleWare;
using EmailService;
using Bootapp.Filters;
using System.Net.Http;

namespace Bootapp
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
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseNpgsql(
                  Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq(Configuration.GetSection("Seq"));
            });

            var emailConfig = Configuration
               .GetSection("EmailConfiguration")
               .Get<EmailConfiguration>();
                    services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, EmailSender>();

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
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
                        ValidIssuer = Configuration["JWT:JwtIssuer"],
                        ValidAudience = Configuration["JWT:JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:JwtKey"])),
                        ClockSkew = TimeSpan.Zero, // remove delay of token when expire
                        
                    };
                });

            services.AddScoped<ConnectionLogFilter>();
            services.AddScoped<ApiAuthFilter>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CustomMapperProfile>();
            });
            // ===== Add MVC ========

            services.AddScoped<IService<app_account>, AccountService>();
            services.AddScoped<IService<app_project>, ProjectService>();
            services.AddScoped<IService<app_datasource>, DatasourceService>();
            services.AddTransient<IService<app_project_table_setting>, TableSettingService>();
            services.AddScoped<IService<app_connection>, ConnectionService>();
            services.AddScoped<AnalyticsService>();
            services.AddScoped<IKeyTokenUtils, KeyTokenUtils>();
            services.AddScoped<IService<app_short_url>, ShortUrlService>();
            services.AddScoped<IService<app_file>, FileService>();
            services.AddScoped<IService<app_file_type>, FileTypeService>();
            services.AddScoped<ProjectUtil>();
            services.AddScoped<IService<app_domain>, DomainService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRoleService, AuthRoleService>();
            //services.AddHostedService<TimedBackgroundHostedService>();


            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:8081", "http://localhost:8080")
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod();
            //        });
            //});


            //----------------------Setup domain filtering -------------------------------------------
            var domainService = services.BuildServiceProvider()
                 .GetService<IService<app_domain>>();

            //Get all the domains that are whitelisted from the database and add them to the CORS policy

            try
            {
                var domains = domainService.GetAll().Select(e => e.domain).ToArray();

                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins(domains)
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                        });
                });
            }
            catch { }
            //--------------------------------------------------------------------------------------

          


            // ------------------- Add background service ---------------------
            services.AddHostedService<AppBackgroundService>();
            // ----------------------------------------------------------------

            services.AddHttpContextAccessor();

            services.AddMvc();

            //enable for dynamic post
            services.AddMvc().AddNewtonsoftJson();


            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            
            var result=dbContext.Database.EnsureCreated();

            //app.UseMiddleware<OriginMiddleWare>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {

                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "client-app";
                    spa.UseVueDevelopmentServer();
                }
                else
                {
                    spa.Options.SourcePath = @"client-app\dist";
                }
            });


//#if (!DEBUG)

//            dbContext.Database.Migrate();
//            dbContext.Database.EnsureCreated();
//#endif
        }
    }
}
