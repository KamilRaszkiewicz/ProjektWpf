using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Infrastructure.Database;
using TestsGenerator.Infrastructure.Services;

namespace TestsGenerator.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            services.AddDbContext<TestsDbContext>(x =>
            {
                x.UseSqlite("Data Source = Tests.db");
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IPdfService, PdfService>();

            return services;
        }
    }
}
