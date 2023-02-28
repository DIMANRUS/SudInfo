//Создано только для миграций, чтобы они могли получить контекст
namespace EfDataAccessLibrary.Factories;
internal class ApplicationDBContextFactory : IDesignTimeDbContextFactory<SudInfoDbContext>
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile("appsettings.json")
        .AddUserSecrets<SudInfoDbContext>()
        .Build();
    public SudInfoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SudInfoDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlExpressDevelop"));
        return new SudInfoDbContext();
    }
}