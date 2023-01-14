namespace SudInfo.Avalonia.Extensions;
internal static class ServiceCollectionExtension
{
    public static IServiceProvider ServiceProvider { get; private set; }
    public static void Init()
    {
        ServiceProvider = new ServiceCollection()
            .AddTransient<ComputersPageViewModel>()
            .AddTransient<ComputerWindowViewModel>()
            .AddTransient<PrintersPageViewModel>()
            .AddTransient<PrinterWindowViewModel>()
            .AddTransient<MonitorsPageViewModel>()
            .AddTransient<MonitorWindowViewModel>()
            .AddTransient<UsersPageViewModel>()
            .AddTransient<UserWindowViewModel>()
            .AddTransient<IMonitorsService, MonitorsService>()
            .AddTransient<IComputersService, ComputersService>()
            .AddTransient<IUsersService, UsersService>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<IDialogService, DialogService>()
            .AddSingleton<IValidationService, ValidationService>()
            .AddTransient<IPrintersService, PrintersService>()
            .BuildServiceProvider();
    }
}