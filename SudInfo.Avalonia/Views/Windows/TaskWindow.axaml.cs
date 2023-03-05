namespace SudInfo.Avalonia.Views.Windows;

public partial class TaskWindow : ReactiveWindow<TaskWindowViewModel>
{
    public TaskWindow()
    {
        InitializeComponent();
        var viewModel = ServiceCollectionExtension.ServiceProvider.GetService<TaskWindowViewModel>();
        DataContext = viewModel;
        InitializeComponent();
    }
}