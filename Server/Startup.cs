using DemoBlazorApp.Server.Entities;
using DemoBlazorApp.Server.Models.Services.Application.Persone;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DemoBlazorApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _policyName = "CorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRazorPages();

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var persistence = Persistence.EfCore;

            switch (persistence)
            {
                case Persistence.EfCore:
                    services.AddDbContextPool<BlazorAppDbContext>(optionBuilder =>
                    {
                        string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                        optionBuilder.UseSqlite(connectionString, options =>
                        {
                            // Abilitazione del connection resiliency (Non è supportato dal provider di Sqlite perchè non è soggetto a errori transienti)
                            // Per informazioni consultare la pagina: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                        });
                    });

                    break;
            }

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Demo API Blazor App",
                    Version = "v1",
                    // Description = "API example that returns the current time",
                    // TermsOfService = new Uri("https://example.com/terms"), 

                    // Contact = new OpenApiContact
                    // {
                    //     Name = "Nominativo contatto",
                    //     Email = "Email contatto",
                    //     Url = new Uri("https://twitter.com/username-contatto"),
                    // },

                    // License = new OpenApiLicense
                    // {
                    //     Name = "Nome licenza API",
                    //     Url = new Uri("https://example.com/license"),
                    // }
                });
            });

            services.AddTransient<IPersonaService, EfCorePersonaService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoApiEfCoreSwagger v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(_policyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}