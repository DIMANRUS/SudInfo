namespace SudInfo.Avalonia.Views.Pages;

public partial class WorkplacesPage : ReactiveUserControl<WorkplacesPageViewModel>
{
    public WorkplacesPage()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}