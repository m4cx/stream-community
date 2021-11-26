using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StreamCommunity.Api.ViewerGames;
using TwitchCommunity.Connector;
using TwitchCommunity.Persistence;

namespace TwitchCommunity.Web
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
            services.AddWebApplication();
            services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(typeof(EnlistmentsController).Assembly));

            services.AddTwitchCommunityPersistence(Configuration);
            services.AddTwitchCommunityConnector(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.CreateScope()
               .ServiceProvider.GetRequiredService<TwitchCommunityDbContext>()
               .Database
               .EnsureCreated();

            var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
            logger.LogInformation("Starting");

            app.AddWebApplication(env);
        }
    }
}
