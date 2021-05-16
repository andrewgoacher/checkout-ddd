using Kata.Domain.Infrastructure.DB;
using Kata.Domain.Services;
using KataApi.Domain.Infrastructure.DB;
using KataApi.Domain.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KataApi.Domain.Infrastructure.Config
{
    public static class Registration
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KataContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("KataDb"));
            });

            services.AddScoped<IBasketStore, BasketStore>();
            services.AddSingleton<IItemService, ItemService>();

            return services;
        }

        public static void EnsureCreated(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<KataContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
