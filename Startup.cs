using GoWMS.Server.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWMS.Server.Data;
using Blazored.Modal;
// ******
// BLAZOR COOKIE Auth Code (begin)
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Localization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
// BLAZOR COOKIE Auth Code (end)
// ******




namespace GoWMS.Server
{
    public class Startup
    {
   

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
    
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

            // Add Controllers for WebAPI
            services.AddControllers();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMudServices();      
            services.AddResponseCaching();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddSingleton<WeatherForecastService>();

            services.AddSingleton<CustomerService>();
            services.AddSingleton<ErpApiService>();
            services.AddSingleton<StoreinService>();
            services.AddSingleton<WhService>();
            services.AddSingleton<ErpService>();
            services.AddSingleton<InvService>();
            services.AddSingleton<InboundService>();
            services.AddSingleton<MasService>();
            services.AddSingleton<WgcService>();
            services.AddSingleton<StoreoutService>();
            services.AddSingleton<ReportService>();
            services.AddSingleton<PublicService>();
            services.AddSingleton<AgvService>();
            services.AddSingleton<UtilityServices>();
            services.AddSingleton<DashService>();
            services.AddSingleton<WcsService>();
            services.AddSingleton<UserServices>();
            services.AddSingleton<PublicFunServices>();

            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();

            services.AddBlazoredModal();

            services.AddServerSideBlazor()
                .AddHubOptions(options =>
                {
                    options.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
                    options.KeepAliveInterval = TimeSpan.FromSeconds(3);
                    options.HandshakeTimeout = TimeSpan.FromMinutes(10);
                });



            // Add Authentication
            // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "gowmauth";
                        options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                        options.EventsType = typeof(Controllers.CookieAuthenticationEvents);

                    });
            services.AddScoped<Controllers.CookieAuthenticationEvents>();

        }

        private RequestLocalizationOptions GetlocalizationOptions()
        {
            var cultures = Configuration.GetSection("Cultures")
                .GetChildren().ToDictionary(x => x.Key, x => x.Value);
            var supportedCultures = cultures.Keys.ToArray();
            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            return localizationOptions;

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization(GetlocalizationOptions());

            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub(options => { options.Transports = HttpTransportType.LongPolling; });
                endpoints.MapFallbackToPage("/_Host");
            });

        }
    }
}
