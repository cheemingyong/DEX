using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using AspNetMaker2017.Models;
using AspNetMaker2017.Controllers;
using static AspNetMaker2017.Models.DEX;
using Microsoft.AspNetCore.Cors;

// Project
namespace AspNetMaker2017
{
    public class Startup
    {
        public static Dictionary<string, Microsoft.Extensions.Primitives.StringValues> CurrencyRates;
        public static String CurrencyRatesOCBCfx;
        public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			if (env.IsDevelopment())
			{

				// This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
				builder.AddApplicationInsightsSettings(developerMode: true);
			}
			Configuration = builder.Build();
		}
		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			// Add framework services.
			services.AddApplicationInsightsTelemetry(Configuration);
			services.AddMvc();
			services.AddSession(o => {
				o.CookieName = ".DEX.Session";
				o.IdleTimeout = TimeSpan.FromMinutes((EW_SESSION_TIMEOUT > 0) ? EW_SESSION_TIMEOUT : 20);
			});
			services.AddAuthentication();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				if (EW_DEBUG_ENABLED) {
					app.UseDeveloperExceptionPage();
					app.UseBrowserLink();
				} else {
					app.UseExceptionHandler("/Home/Error");
				}
			}
			app.UseMiddleware<ExceptionHandler>(); // IMPORTANT: MUST be after UseDeveloperExceptionPage()/UseExceptionHandler()
			app.UseApplicationInsightsExceptionTelemetry();
			app.UseStaticFiles(ew_StaticFileOptions);
			var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
			DEX.Configure(httpContextAccessor, env);
			app.UseSession(); // IMPORTANT: MUST be before UseMvc()
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AutomaticAuthenticate = true,

				//SessionStore = new MemoryCacheTicketStore(),
				ExpireTimeSpan = TimeSpan.FromMinutes(EW_SESSION_TIMEOUT)
			});
			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
			});
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(new System.Globalization.CultureInfo("en-US")),

				// Formatting numbers, dates, etc.
				SupportedCultures = new List<System.Globalization.CultureInfo>
				{
					new System.Globalization.CultureInfo("en-US")
				}
			});
		}
	}
}
