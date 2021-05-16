using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Kata.Domain.Infrastructure.Serialisation;
using Kata.Domain.Services;
using KataApi.Domain.Infrastructure.Config;
using KataApi.Domain.Infrastructure.DB;
using KataApi.Middleware;
using KataApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace KataApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// 
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // todo: This is a bit of a hack just to progress with the work
                    // ideally need to figure out what the right approach would be
                    var defaults = CustomSerialisationOptions.Get(new ItemService());
                    foreach (var converter in defaults.Converters)
                    {
                        options.JsonSerializerOptions.Converters.Add(converter);
                    }
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KataApi", Version = "v1", Description = "A web api implementing the checkout kata" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Database stuff
            services.Configure<BasketStoreSettings>(
                Configuration.GetSection(nameof(BasketStoreSettings)));

            // Domain service registration
            services.AddScoped<IBasketStore, BasketStore>();
            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<DiscountRuleService>();
            services.AddScoped<CheckoutApplicationService>();


        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// 
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KataApi v1"));
            }

            app.UseExceptionHandler(err =>
            {
                err.Run(async ctx => await ExceptionMiddleware.Run(ctx));
            });

            MapDomainModels(app.ApplicationServices);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void MapDomainModels(IServiceProvider provider)
        {
            BasketStore.RegisterClasses(provider.GetService<IItemService>());
        }
    }
}