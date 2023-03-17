using Monitor = SudInfo.EfDataAccessLibrary.Models.Monitor;
using TaskEntity = SudInfo.EfDataAccessLibrary.Models.TaskEntity;

namespace SudInfo.EfDataAccessLibrary.Contexts;
public class SudInfoDbContext : DbContext
{
    public SudInfoDbContext()
    {
        Database.Migrate();
    }

    #region Configuration
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source = SudInfoDatabase.db");
    }
    #endregion

    #region Tables
    public DbSet<User> Users => Set<User>();
    public DbSet<Computer> Computers => Set<Computer>();
    public DbSet<Printer> Printers => Set<Printer>();
    public DbSet<Monitor> Monitors => Set<Monitor>();
    public DbSet<Rutoken> Rutokens => Set<Rutoken>();
    public DbSet<Periphery> Peripheries => Set<Periphery>();
    public DbSet<Server> Servers => Set<Server>();
    public DbSet<ServerRack> ServerRacks => Set<ServerRack>();
    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
    public DbSet<PasswordEntity> Passwords => Set<PasswordEntity>();
    #endregion
}