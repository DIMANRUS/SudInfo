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
            .AddTransient<RutokensPageViewModel>()
            .AddTransient<RutokenWindowViewModel>()
            .AddTransient<PeripheryPageViewModel>()
            .AddTransient<PeripheryWindowViewModel>()
            .AddTransient<WorkplacesPageViewModel>()
            .AddTransient<IMonitorService, MonitorService>()
            .AddTransient<IComputerService, ComputerService>()
            .AddTransient<IUserService, UserService>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<IDialogService, DialogService>()
            .AddSingleton<IValidationService, ValidationService>()
            .AddTransient<IPrinterService, PrinterService>()
            .AddTransient<IRutokenService, RutokenService>()
            .AddTransient<IPeripheryService, PeripheryService>()
            .BuildServiceProvider();
    }
}