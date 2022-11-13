using SudInfo.Avalonia.Extensions;

namespace SudInfo.Avalonia.ViewModels;

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
            .Select(current => !(current is ComputersPageViewModel));

        OpenComputersPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<ComputersPageViewModel>()),
                canOpenedComputersPage);
        #endregion

        #region Printers Page
        var canOpenedPrintersPage = this
            .WhenAnyObservable(x => x.Router.CurrentViewModel)
            .Select(current => !(current is PrintersPageViewModel));

        OpenPrintersPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<PrintersPageViewModel>()),
                canOpenedPrintersPage);
        #endregion

        #region Settings Window

        OpenSettingsWindow = ReactiveCommand.CreateFromTask(ServiceCollectionExtension.ServiceProvider.GetService<INavigationService>().ShowSettingsWindowDialog);
        #endregion

        #endregion
    }
    #endregion

    #region IScreen Realization
    [Reactive]
    public RoutingState Router { get; set; } = new();
    #endregion

    #region Commands
    public ICommand OpenComputersPage { get; private init; }
    public ICommand OpenPrintersPage { get; private init; }
    public ICommand OpenSettingsWindow { get; private init; }
    public ICommand OpenMonitorsPage { get; private init; }
    #endregion
}