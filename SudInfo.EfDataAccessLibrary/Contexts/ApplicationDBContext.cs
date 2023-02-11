using Monitor = SudInfo.EfDataAccessLibrary.Models.Monitor;

namespace SudInfo.EfDataAccessLibrary.Contexts;
public class ApplicationDBContext : DbContext
{
    #region Private variables
    private readonly IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile("appsettings.json")
        .AddUserSecrets<ApplicationDBContext>()
        .Build();
    #endregion

    #region Configuration
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(configuration.GetConnectionString("SqlExpressDevelop"));
    }
    #endregion

    #region Tables
    public DbSet<User> Users { get; set; }
    public DbSet<Computer> Computers { get; set; }
    public DbSet<Printer> Printers { get; set; }
    public DbSet<Monitor> Monitors { get; set; }
    public DbSet<Rutoken> Rutokens { get; set; }
    public DbSet<Periphery> Peripheries { get; set; }
    public DbSet<Server> Servers { get; set; }
    #endregion
}