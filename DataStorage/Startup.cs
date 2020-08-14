using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspBlog.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using AspBlog.Models;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog;
using AspBlog.Extensions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using AspNet.Security.ApiKey.Providers;
using AspNet.Security.ApiKey.Providers.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AspNet.Security.ApiKey.Providers.Events;
using System.Security.Claims;
using System.Net;

namespace AspBlog
{
    public class Startup
    {
        //private readonly ApplicationDbContext databaza;//



        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory
            //,ApplicationDbContext databaza
            )
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/Logy/nlog.config.txt"));
            Configuration = configuration;
            // this.databaza = databaza;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //    services.AddDbContext<DatabazovyKontext>(options =>
            //       options.UseSqlServer(
            //         Configuration.GetConnectionString("DefaultConnection")));


            services.AddSingleton<ApiKeyValidation>();



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });





            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                    Configuration.GetConnectionString("Databaza")));
            //  services.AddDefaultIdentity<IdentityUser>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>(); // pridane; 

            // services.AddDbContext<ApplicationDbContext>(optionsAction: opt => opt.UseInMemoryDatabase());
            //  var databaza = services.GetService<ApplicationDbContext>();
            services.ConfigureLoggerService();
           //  services.AddAuthentication();
         /* var aut = services.AddAuthentication(options =>
             {
                options.DefaultAuthenticateScheme = ApiKeyDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = ApiKeyDefaults.AuthenticationScheme;

             });
            aut.AddIdentityCookies();
            aut.AddApiKey(options =>
      {
          options.Header = "X-API-KEY";
          options.HeaderKey = String.Empty;
          options.Events = new ApiKeyEvents
          {
              OnApiKeyValidated = context =>
              {
                   // MyService myService = new MyService(serviceProvider);
                   // myService.Test1(context);
                   //  ApiKeyValidation apiKeyValidation = new ApiKeyValidation();
                   //   apiKeyValidation.Validacia1(context);
                   

                   if (context.ApiKey == "Miro")
                  {
                      context.Principal = new ClaimsPrincipal();

                      context.Success();
                  }
                  else if (context.ApiKey == "789")
                  {
                      throw new NotSupportedException("You must upgrade.");
                  }

                  return System.Threading.Tasks.Task.CompletedTask;
              },
              OnChallenge = context =>
              {
                  if (context.AuthenticateFailure is NotSupportedException)
                  {
                      context.StatusCode = HttpStatusCode.UpgradeRequired;
                  }

                  return System.Threading.Tasks.Task.CompletedTask;
              }
          };
      });*/


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

            });

            services.AddSwaggerGen(options =>
            {

                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "apiKey", Enumerable.Empty<string>() } });

                options.AddSecurityDefinition("apiKey", new ApiKeyScheme
                {

                    Description = "API Key Authentication",
                    In = "header",
                    Name = "X-Api-Key",
                    Type = "apiKey"

                });
            });

            // var provider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                               Contracts.ILoggerManager logger,
                              RoleManager<IdentityRole> spravceRoli,
                              UserManager<IdentityUser> spravceUzivatelu,
                              IServiceProvider serviceProvider
                             )
        {


            serviceProvider.GetService<ApplicationDbContext>();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }



            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                //c.EnableApiKeySupport("X-ApiKey", "header");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;


            });
            //    spravceRoli.CreateAsync(new IdentityRole("admin")).Wait();
            //   IdentityUser uzivatel = spravceUzivatelu.FindByEmailAsync("admin@hobi.cz").Result;
            //    spravceUzivatelu.AddToRoleAsync(uzivatel, "admin").Wait();

            //   using (var context = app.ApplicationServices.GetService<ApplicationDbContext>())
            // {
            //    context.Database.EnsureDeleted();
            //    context.Database.EnsureCreated();
            // }

        }
    }
}
