using XU9YYJ_HFT_2021221.Logic.Interfaces;
using XU9YYJ_HFT_2021221.Logic.Services;
using XU9YYJ_HFT_2021221.Repository.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XU9YYJ_HFT_2021221.Logic.Infrastructure
{
    public static class BLInitialization
    {
        public static void InitBlServices(IServiceCollection services) //RegisterServices
        {
            RepoInitialization.InitRepoServices(services);

            services.AddScoped<IOrderLogic, OrderLogic>();
            services.AddScoped<ISupplierLogic, SupplierLogic>();
            services.AddScoped<IItemLogic, ItemLogic>();
            
        }
    }
}
