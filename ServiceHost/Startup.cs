using AccountManagement.Infrastructure.Configuration;
using AdminManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;
using Framework.Application.Authentication;
using Framework.Application.Hashing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreManagement.Infrastructure.Configuration;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ServiceHost
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
            services.AddHttpContextAccessor();
            services.AddTransient<IAuthHelper, AuthHelper>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            StoreManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            AdminManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            CommentManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            AccountManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            DiscountManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = "/Account";
                    o.LogoutPath = "/Account/Logout";
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
                });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
