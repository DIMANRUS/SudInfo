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
            .AddSingleton<PhonesPageViewModel>()
    #endregion
    #region Window view models
            .AddTransient<AppWindowViewModel>()
            .AddTransient<CartridgeWindowViewModel>()
            .AddTransient<ComputerWindowViewModel>()
            .AddTransient<ContactWindowViewModel>()
            .AddTransient<PasswordWindowViewModel>()
            .AddTransient<TaskWindowViewModel>()
            .AddTransient<ServerRackWindowViewModel>()
            .AddTransient<PeripheryWindowViewModel>()
            .AddTransient<ServerWindowViewModel>()
            .AddTransient<RutokenWindowViewModel>()
            .AddTransient<UserWindowViewModel>()
            .AddTransient<MonitorWindowViewModel>()
            .AddTransient<PrinterWindowViewModel>()
            .AddTransient<PhoneWindowViewModel>()
    #endregion
    #region Services
            .AddTransient<ExcelService>()
            .AddTransient<PrinterService>()
            .AddTransient<RutokenService>()
            .AddTransient<PeripheryService>()
            .AddTransient<ServerService>()
            .AddTransient<CartridgeService>()
            .AddTransient<ContactService>()
            .AddTransient<PasswordService>()
            .AddTransient<AppService>()
            .AddTransient<TaskService>()
            .AddTransient<UserService>()
            .AddTransient<MonitorService>()
            .AddTransient<ServerRackService>()
            .AddSingleton<NavigationService>()
            .AddTransient<ComputerService>()
            .AddTransient<PhoneService>()
    #endregion
            .AddDbContext<SudInfoDatabaseContext>()
            .BuildServiceProvider();
}