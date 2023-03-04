namespace SudInfo.Avalonia.Extensions;
internal static class ServiceCollectionExtension
{
    public static IServiceProvider ServiceProvider { get; private set; }
    public static void Init()
    {
        ServiceProvider = new ServiceCollection()
            .AddSingleton<ComputersPageViewModel>()
            .AddTransient<ComputerWindowViewModel>()
            .AddSingleton<PrintersPageViewModel>()
            .AddTransient<PrinterWindowViewModel>()
            .AddSingleton<MonitorsPageViewModel>()
            .AddTransient<MonitorWindowViewModel>()
            .AddSingleton<UsersPageViewModel>()
            .AddTransient<UserWindowViewModel>()
            .AddSingleton<RutokensPageViewModel>()
            .AddTransient<RutokenWindowViewModel>()
            .AddSingleton<PeripheryPageViewModel>()
            .AddTransient<PeripheryWindowViewModel>()
            .AddSingleton<WorkplacesPageViewModel>()
            .AddSingleton<ServersPageViewModel>()
            .AddSingleton<TasksPageViewModel>()
            .AddTransient<ServerWindowViewModel>()
            .AddTransient<ServerRackWindowViewModel>()
            .AddTransient<MonitorService>()
            .AddTransient<ComputerService>()
            .AddTransient<UserService>()
            .AddSingleton<NavigationService>()
            .AddSingleton<DialogService>()
            .AddTransient<PrinterService>()
            .AddTransient<RutokenService>()
            .AddTransient<PeripheryService>()
            .AddTransient<ServerService>()
            .AddTransient<ServerRackService>()
            .AddTransient<TaskService>()
            .BuildServiceProvider();
    }
}