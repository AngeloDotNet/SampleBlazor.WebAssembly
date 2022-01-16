using DemoBlazorApp.Shared.Models.Enums;
using DemoBlazorApp.Shared.Models.Options;
using DemoBlazorApp.Server.Models.Services.Infrastructure;
using DemoBlazorApp.Server.Models.Services.Application.Persone;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoBlazorApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRazorPages();

            var persistence = Persistence.EfCore;

            switch (persistence)
            {
                case Persistence.EfCore:
                    services.AddDbContextPool<BlazorAppDbContext>(optionBuilder =>
                    {
                        string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                        optionBuilder.UseSqlite(connectionString, options =>
                        {
                            // Abilitazione del connection resiliency (Non � supportato dal provider di Sqlite perch� non � soggetto a errori transienti)
                            // Per informazioni consultare la pagina: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                        });
                    });

                    break;
            }
            services.AddTransient<IPersonaService, EfCorePersonaService>();

            //Options
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}