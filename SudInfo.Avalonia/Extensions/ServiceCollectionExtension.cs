namespace SudInfo.Avalonia.Extensions;
internal static class ServiceCollectionExtension
{
    public static IServiceProvider ServiceProvider { get; private set; }
    public static void Init()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<App>()
            .Build();

        ServiceProvider = new ServiceCollection()
            .AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("SqlExpressDevelop"));
                //option.UseSqlServer(configuration.GetConnectionString("UbuntuServerDevelop"));
            })
            .AddTransient<ComputersPageViewModel>()
            .AddTransient<ComputerWindowViewModel>()
            .AddTransient<PrintersPageViewModel>()
            .AddTransient<PrinterWindowViewModel>()
            .AddTransient<MonitorsPageViewModel>()
            .AddTransient<IComputersService, ComputersService>()
            .AddTransient<IEmployeeService, EmployeeService>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<IDialogService, DialogService>()
            .AddSingleton<IValidationService, ValidationService>()
            .AddTransient<IPrintersService, PrintersService>()
            .BuildServiceProvider();
    }
}