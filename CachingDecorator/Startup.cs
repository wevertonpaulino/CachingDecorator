using CachingDecorator.Repositories;
using CachingDecorator.Repositories.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CachingDecorator
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
            services.AddControllers();
            services.AddMemoryCache();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CachingDecorator",
                    Description = "A simple example of Caching with the Decorator Pattern",
                    Contact = new OpenApiContact
                    {
                        Name = "Weverton Cesar Paulino",
                        Email = "wevertoncesar@gmail.com",
                    }
                });
            });
            services.AddScoped<ICarRepository, CarRepository>();

            EnableDecorator(services);
            //EnableDecoratorHandsOn(services);
        }

        private void EnableDecorator(IServiceCollection services)
        {
            services.Decorate<ICarRepository, CarCachingDecorator>();
        }

        private void EnableDecoratorHandsOn(IServiceCollection services)
        {
            services.AddScoped<CarRepository>();
            services.AddScoped<ICarRepository, CarCachingDecoratorHandsOn<CarRepository>>();
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

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "CachingDecorator");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
