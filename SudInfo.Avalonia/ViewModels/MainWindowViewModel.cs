using SudInfo.Avalonia.Extensions;

namespace SudInfo.Avalonia.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    #region Constructors
    public MainWindowViewModel()
    {
        Router.Navigate.Execute(ServiceCollectionExtension.ServiceProvider.GetService<ComputersPageViewModel>());

        #region Navigation

        #region Computer Page
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
    private RoutingState _router = new();
    public RoutingState Router
    {
        get => _router;
        set => this.RaiseAndSetIfChanged(ref _router, value);
    }
    #endregion

    #region Commands
    public ICommand OpenComputersPage { get; private set; }
    public ICommand OpenPrintersPage { get; private set; }
    public ICommand OpenSettingsWindow { get; private set; }
    #endregion
}