﻿namespace SudInfo.Avalonia.Extensions;

internal static class ServiceCollectionExtension
{
    public static readonly IServiceProvider ServiceProvider = ServiceProvider = new ServiceCollection()
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
            .AddTransient<TaskWindowViewModel>()
            .AddSingleton<PasswordsPageViewModel>()
            .AddTransient<PasswordWindowViewModel>()
            .AddTransient<PasswordService>()
            .AddTransient<AppService>()
            .AddSingleton<AppsPageViewModel>()
            .AddTransient<ContactService>()
            .AddSingleton<ContactsPageViewModel>()
            .AddTransient<AppWindowViewModel>()
            .AddSingleton<CartridgesPageViewModel>()
            .AddTransient<CartridgeService>()
            .AddTransient<CartridgeWindowViewModel>()
            .AddTransient<ContactWindowViewModel>()
            .AddSingleton<ExcelService>()
            .BuildServiceProvider();
}