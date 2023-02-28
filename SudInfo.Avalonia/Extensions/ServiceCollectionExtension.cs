namespace SudInfo.Avalonia.Extensions;
internal static class ServiceCollectionExtension
{
    public static IServiceProvider ServiceProvider { get; private set; }
    public static void Init()
    {
        ServiceProvider = new ServiceCollection()
            .AddSingleton<ComputersPageViewModel>()
            .AddScoped<ComputerWindowViewModel>()
            .AddSingleton<PrintersPageViewModel>()
            .AddScoped<PrinterWindowViewModel>()
            .AddSingleton<MonitorsPageViewModel>()
            .AddScoped<MonitorWindowViewModel>()
            .AddSingleton<UsersPageViewModel>()
            .AddScoped<UserWindowViewModel>()
            .AddSingleton<RutokensPageViewModel>()
            .AddScoped<RutokenWindowViewModel>()
            .AddSingleton<PeripheryPageViewModel>()
            .AddScoped<PeripheryWindowViewModel>()
            .AddSingleton<WorkplacesPageViewModel>()
            .AddSingleton<ServersPageViewModel>()
            .AddScoped<ServerWindowViewModel>()
            .AddScoped<ServerRackWindowViewModel>()
            .AddScoped<MonitorService>()
            .AddScoped<ComputerService>()
            .AddScoped<UserService>()
            .AddSingleton<NavigationService>()
            .AddSingleton<DialogService>()
            .AddSingleton<ValidationService>()
            .AddScoped<PrinterService>()
            .AddScoped<RutokenService>()
            .AddScoped<PeripheryService>()
            .AddScoped<ServerService>()
            .AddScoped<ServerRackService>()
            .BuildServiceProvider();
    }
}