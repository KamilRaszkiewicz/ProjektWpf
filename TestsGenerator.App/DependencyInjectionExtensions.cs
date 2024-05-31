using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.App.Services;

namespace TestsGenerator.App
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApp(this IServiceCollection services)
        {
            services.AddSingleton<QuestionsService>();
            services.AddSingleton<TemplatesService>();

            return services;
        }
    }
}
