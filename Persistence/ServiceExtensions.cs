using Application.Interfaces;
using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IRppDbContext, RPPDbContext>();
            //services.AddTransient<>();
            //services.AddSingleton<>();
        }
    }
}
