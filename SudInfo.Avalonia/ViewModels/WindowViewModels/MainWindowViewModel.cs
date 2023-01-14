namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    #region Constructors
    public MainWindowViewModel()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<ComputersPageViewModel>());

        #region Navigation

        #region Computers Page
        var canOpenedComputersPage = this
            .WhenAnyObservable(x => x.Router.CurrentViewModel)
            .Select(current => current is not ComputersPageViewModel);

        OpenComputersPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<ComputersPageViewModel>()), canOpenedComputersPage);
        #endregion

        #region Printers Page
        var canOpenedPrintersPage = this
            .WhenAnyObservable(x => x.Router.CurrentViewModel)
            .Select(current => current is not PrintersPageViewModel);

        OpenPrintersPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<PrintersPageViewModel>()), canOpenedPrintersPage);
        #endregion

        #region Monitors Page
        var canOpenedMonitorsPage = this
            .WhenAnyObservable(x => x.Router.CurrentViewModel)
            .Select(current => current is not MonitorsPageViewModel);

        OpenMonitorsPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<MonitorsPageViewModel>()), canOpenedMonitorsPage);
        #endregion

        #region Users Page
        var canOpenedUsersPage = this
            .WhenAnyObservable(x => x.Router.CurrentViewModel)
            .Select(current => current is not UsersPageViewModel);

        OpenUsersPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<UsersPageViewModel>()), canOpenedUsersPage);
        #endregion

        #region Settings Window

        OpenSettingsWindow = ReactiveCommand.CreateFromTask(ServiceCollectionExtension.ServiceProvider.GetService<INavigationService>().ShowSettingsWindowDialog);
        #endregion

        #endregion
    }
    #endregion

    #region IScreen Realization
    public RoutingState Router { get; set; } = new();
    #endregion

    #region Commands
    public ICommand OpenComputersPage { get; private init; }
    public ICommand OpenPrintersPage { get; private init; }
    public ICommand OpenSettingsWindow { get; private init; }
    public ICommand OpenMonitorsPage { get; private init; }
    public ICommand OpenUsersPage { get; private init; }
    #endregion
}