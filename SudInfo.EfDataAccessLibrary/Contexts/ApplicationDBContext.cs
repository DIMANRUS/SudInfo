namespace SudInfo.EFDataAccessLibrary.Contexts;
public class ApplicationDBContext : DbContext
{
    #region Constructors
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public ApplicationDBContext() { }
    #endregion

    #region Configuration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = 10.0.0.2; Database = SudInfo; User ID = sa; Password = admin2022@;Connect Timeout=30;");
        // optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SudInfo;Connect Timeout=30;");
    }
    #endregion

    #region Table Properties
    public DbSet<Computer> Computers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    #endregion
}