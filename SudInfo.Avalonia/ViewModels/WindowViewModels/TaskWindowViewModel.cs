namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class TaskWindowViewModel : BaseViewModel
{
    public readonly NavigationService _navigationService;
    public TaskEntity Task { get; set; } = new();
    public TaskWindowViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}