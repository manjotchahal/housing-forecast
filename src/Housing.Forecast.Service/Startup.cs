using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Housing.Forecast.Context;
using Housing.Forecast.Context.Repos;
using Housing.Forecast.Context.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Housing.Forecast.Service
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
            services.AddDbContext<ForecastContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ForecastDB")));
            services.AddTransient<IRepo<User>, UserRepo>();
            services.AddTransient<IRepo<Room>, RoomRepo>();
            services.AddTransient<IRepo<Snapshot>, SnapshotRepo>();
            services.AddMvc();
            services.AddSingleton<IQueueClient>(qc =>
              new QueueClient(
                Environment.GetEnvironmentVariable("SERVICE_BUS_CONNECTION_STRING"),
                Environment.GetEnvironmentVariable("SERVICE_BUS_QUEUE_NAME")
              )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddApplicationInsights(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
