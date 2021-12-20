using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StreamCommunity.Api;
using StreamCommunity.Api.ViewerGames;
using StreamCommunity.Persistence;
using StreamCommunity.Twitch;

namespace StreamCommunity.Host
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
            services.AddControllers()
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(typeof(EnlistmentsController).Assembly));

            services.AddStreamCommunityPersistence(Configuration);
            services.AddTwitchCommunityConnector(Configuration);

            services.AddMediatR(typeof(ApiHub).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<StreamCommunityDbContext>()
                .Database
                .Migrate();

            var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
            logger.LogInformation("Starting");

            app.AddWebApplication(env);
        }
    }
}