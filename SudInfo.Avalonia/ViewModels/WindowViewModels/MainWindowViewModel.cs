namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    #region Constructors
    public MainWindowViewModel()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<ComputersPageViewModel>());
    }
    #endregion

    #region IScreen Realization
    public RoutingState Router { get; set; } = new();
    #endregion

    #region Public Methods
    public void OpenUsersPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<UsersPageViewModel>());
    }
    public void OpenComputersPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<ComputersPageViewModel>());
    }
    public void OpenPrintersPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<PrintersPageViewModel>());
    }
    public async void OpenSettingsWindow()
    {
        await ServiceCollectionExtension.ServiceProvider.GetService<NavigationService>().ShowSettingsWindowDialog();
    }
    public void OpenMonitorsPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<MonitorsPageViewModel>());
    }
    public void OpenRutokensPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<RutokensPageViewModel>());
    }
    public void OpenPeripheryPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<PeripheryPageViewModel>());
    }
    public void OpenWorkplacesPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<WorkplacesPageViewModel>());
    }
    public void OpenServersPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<ServersPageViewModel>());
    }
    public void OpenTasksPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<TasksPageViewModel>());
    }
    public void OpenPasswordsPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<PasswordsPageViewModel>());
    }
    #endregion
}