using Core.Interfaces;
using Infrastructure.Data.Csv;
using Infrastructure.Data.Ef;
using Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, 
        string contentRootPath)
    {         
        var dataSource = configuration.GetSection("DataSource").Get<DataSourceOptions>() ?? new DataSourceOptions();

        if (dataSource.Provider.Equals("Csv", StringComparison.OrdinalIgnoreCase))
        {
            services.Configure<CsvOptions>(opts =>
            {
                var path = configuration.GetSection("DataSource:Csv:FilePath").Value;

                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new InvalidOperationException("Die CSV Datenquelle wurde ausgewählt, aber der Konfigurationswert 'DataSource:Csv:FilePath' ist leer");
                }

                opts.FilePath = Path.GetFullPath(Path.Combine(contentRootPath, path));
            });

            services.AddSingleton<PersonCsvReader>();
            services.AddScoped<IPersonRepository, CsvPersonRepository>();
        }
        else if (dataSource.Provider.Equals("Ef", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<PersonDb>(options => options.UseSqlite(dataSource.Ef.ConnectionString));
            services.AddScoped<IPersonRepository, EfPersonRepository>();
        }
        else
        {
            throw new InvalidOperationException($"Unbekannte Datenquelle'{dataSource.Provider}'");
        }

        return services;
    }
}