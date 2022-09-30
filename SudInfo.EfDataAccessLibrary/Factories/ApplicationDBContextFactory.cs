//Создано только для миграций, чтобы они могли получить контекст
namespace EFDataAccessLibrary.Factories;
internal class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
{
    public ApplicationDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
        //optionsBuilder.UseSqlServer("Data Source = 10.0.0.2; Initial Catalog = SudInfo; User ID = sa; Password = admin2022@;Integrated Security=True;Connect Timeout=30;");
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SudInfo;Integrated Security=True;Connect Timeout=30;");
        return new ApplicationDBContext(optionsBuilder.Options);
    }
}