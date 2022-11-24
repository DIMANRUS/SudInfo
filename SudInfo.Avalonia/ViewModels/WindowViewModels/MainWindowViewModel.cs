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
        var computerPageViewModel = ServiceCollectionExtension.ServiceProvider.GetService<ComputersPageViewModel>();
        OpenComputersPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(computerPageViewModel), canOpenedComputersPage);
        #endregion

        #region Printers Page
        var canOpenedPrintersPage = this
            .WhenAnyObservable(x => x.Router.CurrentViewModel)
            .Select(current => current is not PrintersPageViewModel);

        var printersPageViewModel = ServiceCollectionExtension.ServiceProvider.GetService<PrintersPageViewModel>();

        OpenPrintersPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(printersPageViewModel), canOpenedPrintersPage);
        #endregion

        #region Monitors Page
        var canOpenedMonitorsPage = this
            .WhenAnyObservable(x => x.Router.CurrentViewModel)
            .Select(current => current is not MonitorsPageViewModel);

        var monitorsPageViewModel = ServiceCollectionExtension.ServiceProvider.GetService<MonitorsPageViewModel>();

        OpenMonitorsPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(monitorsPageViewModel), canOpenedMonitorsPage); ;
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
    #endregion
}