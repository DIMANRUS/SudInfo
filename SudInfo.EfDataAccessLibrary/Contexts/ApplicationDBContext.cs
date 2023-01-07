using Monitor = SudInfo.EfDataAccessLibrary.Models.Monitor;

namespace SudInfo.EFDataAccessLibrary.Contexts;
public class ApplicationDBContext : DbContext
{
    #region Constructors
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }
    public ApplicationDBContext()
    {
    }
    #endregion

    #region Tables
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Computer> Computers { get; set; }
    public DbSet<Printer> Printers { get; set; }
    public DbSet<Monitor> Monitors { get; set; }
    #endregion
}