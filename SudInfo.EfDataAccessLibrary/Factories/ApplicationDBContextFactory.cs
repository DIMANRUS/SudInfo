//Создано только для миграций, чтобы они могли получить контекст
using Microsoft.Extensions.Configuration;

namespace EFDataAccessLibrary.Factories;
internal class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile("appsettings.json")
        .AddUserSecrets<ApplicationDBContext>()
        .Build();
    public ApplicationDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlExpressDevelop"));
        return new ApplicationDBContext();
    }
}