using AccountManagement.Infrastructure.Configuration;
using AdminManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;
using Framework.Application.Authentication;
using Framework.Application.Hashing;
using Framework.Application.ZarinPal;
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
using TicketManagement.Infrastructure.Configuration;

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
            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

            BlogManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            StoreManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            AdminManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            TicketManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            CommentManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            AccountManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            DiscountManagementBootstrapper.Configuration(services, Configuration.GetConnectionString("RadMarketConnection"));
            

            #region Config Authentication

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = "/";
                    o.LogoutPath = "/Logout";
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
                });

            #endregion

            #region html encoder

            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));

            #endregion

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
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapFallback(context => {
                //    context.Response.Redirect("/NotFound");
                //    return Task.CompletedTask;
                //});
            });
        }
    }
}
