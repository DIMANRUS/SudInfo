namespace SudInfo.Avalonia.Extensions;

internal static class ServiceCollectionExtension
{
    public static readonly IServiceProvider ServiceProvider = new ServiceCollection()
    #region Page view models
            .AddSingleton<ComputersPageViewModel>()
            .AddSingleton<PrintersPageViewModel>()
            .AddSingleton<MonitorsPageViewModel>()
            .AddSingleton<UsersPageViewModel>()
            .AddSingleton<RutokensPageViewModel>()
            .AddSingleton<PeripheryPageViewModel>()
            .AddSingleton<WorkplacesPageViewModel>()
            .AddSingleton<ServersPageViewModel>()
            .AddSingleton<TasksPageViewModel>()
            .AddSingleton<PasswordsPageViewModel>()
            .AddSingleton<AppsPageViewModel>()
            .AddSingleton<ContactsPageViewModel>()
            .AddSingleton<CartridgesPageViewModel>()
    #endregion
    #region Window view models
            .AddScoped<AppWindowViewModel>()
            .AddScoped<CartridgeWindowViewModel>()
            .AddScoped<ComputerWindowViewModel>()
            .AddScoped<ContactWindowViewModel>()
            .AddScoped<PasswordWindowViewModel>()
            .AddScoped<TaskWindowViewModel>()
            .AddScoped<ServerRackWindowViewModel>()
            .AddScoped<PeripheryWindowViewModel>()
            .AddScoped<ServerWindowViewModel>()
            .AddScoped<RutokenWindowViewModel>()
            .AddScoped<UserWindowViewModel>()
            .AddScoped<MonitorWindowViewModel>()
            .AddScoped<PrinterWindowViewModel>()
    #endregion
    #region Services
            .AddScoped<ExcelService>()
            .AddScoped<PrinterService>()
            .AddScoped<RutokenService>()
            .AddScoped<PeripheryService>()
            .AddScoped<ServerService>()
            .AddScoped<CartridgeService>()
            .AddScoped<ContactService>()
            .AddScoped<PasswordService>()
            .AddScoped<AppService>()
            .AddScoped<TaskService>()
            .AddScoped<UserService>()
            .AddScoped<MonitorService>()
            .AddScoped<ServerRackService>()
            .AddSingleton<NavigationService>()
            .AddScoped<ComputerService>()
    #endregion
            .AddScoped<SudInfoDatabaseContext>()
            .BuildServiceProvider();
}