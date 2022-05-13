using XU9YYJ_HFT_2021221.Data.DbContexts;

using XU9YYJ_HFT_2021221.Repository.Interfaces;
using XU9YYJ_HFT_2021221.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU9YYJ_HFT_2021221.Repository.Infrastructure
{
    public static class RepoInitialization
    {
        public static void InitRepoServices(IServiceCollection services) //RegisterServices
        {
            services.AddScoped((sp) => new XU9YYJ_DbContext());
            
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
        }
    }
}
