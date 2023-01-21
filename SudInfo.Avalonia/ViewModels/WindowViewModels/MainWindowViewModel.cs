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
    public void OpenSettingsWindow()
    {
        ServiceCollectionExtension.ServiceProvider.GetService<INavigationService>().ShowSettingsWindowDialog();
    }
    public void OpenMonitorsPage()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<MonitorsPageViewModel>());
    }
    #endregion 
}