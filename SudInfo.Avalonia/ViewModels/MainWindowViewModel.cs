using System.Collections;

namespace SudInfo.Avalonia.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public MainWindowViewModel()
    {
        Router.Navigate.Execute(new ComputersPageViewModel());
        var canOpenedComputersPage = this
            .WhenAnyObservable(x => x.Router.CurrentViewModel)
            .Select(current => !(current is ComputersPageViewModel));

        _openComputersPage = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new ComputersPageViewModel()),
                canOpenedComputersPage);
    }

    private readonly ReactiveCommand<Unit, IRoutableViewModel> _openComputersPage;
    private RoutingState _router = new();

    public RoutingState Router
    {
        get => _router;
        set => this.RaiseAndSetIfChanged(ref _router, value);
    }
    public ICommand OpenComputersPage => _openComputersPage;
}