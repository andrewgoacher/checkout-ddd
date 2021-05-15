using System;
using Kata.Domain.Services;
using KataApi.Domain.Infrastructure.Config;
using KataApi.Domain.Infrastructure.DB;
using KataApi.Filters;
using KataApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace KataApi
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
            services.AddControllers(c =>
            {
                c.Filters.Add(new GlobalExceptionFilter());
            });

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "KataApi", Version = "v1", Description = "A web api implementing the checkout kata" }); });

            // Database stuff
            services.Configure<BasketStoreSettings>(
                Configuration.GetSection(nameof(BasketStoreSettings)));

            // Domain service registration
            services.AddScoped<IBasketStore, BasketStore>();
            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<DiscountRuleService>();
            services.AddScoped<CheckoutApplicationService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KataApi v1"));
            }

            MapDomainModels(app.ApplicationServices);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void MapDomainModels(IServiceProvider provider)
        {
            BasketStore.RegisterClasses();
        }
    }
}