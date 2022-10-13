using System.Diagnostics.Metrics;

namespace SudInfo.Avalonia.ViewModels;

public class ComputersPageViewModel : ReactiveObject, IRoutableViewModel
{
    public string? UrlPathSegment => "/computers";

    public IScreen HostScreen { get; }

    public IEnumerable<Computer> Computers => new List<Computer>()
    {
        new()
        {
            Ip = "20.44.23.22",
            Employee = new()
            {
                FirstName = "Дмитрий"
            }
        }
    };

    public ComputersPageViewModel()
    {

        _editComputer = ReactiveCommand.Create(
                () => { new MainWindow().Show(); });
    }

    public ICommand EditComputer => _editComputer;
    private readonly ReactiveCommand<Unit, Unit> _editComputer;
}