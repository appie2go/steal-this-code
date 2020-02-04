using Dispatching.Aaa.Configuration;
using Dispatching.Api.Configuration;
using Dispatching.Broker.Configuration;
using Dispatching.Configuration;
using Dispatching.Persistence.Configuration;
using Dispatching.ReadModel.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dispatching.Api
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
            var readModelConnectionString = "todo";
            var writeModelConnectionString = "todo";

            services
                .UseDispatching()
                .UseDispatchingRestApi()
                .UseDispatchingBroker()
                .UseAaaTrafficInformation()
                .UseDispatchingPersistenceAdapters(writeModelConnectionString)
                .UseDispatchingReadModel(readModelConnectionString)
                .AddControllers()
                .AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDispatchingBroker();
        }
    }
}
