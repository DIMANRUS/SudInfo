namespace SudInfo.Avalonia.Views.Pages;
public partial class PrintersPage : ReactiveUserControl<PrintersPageViewModel>
{
    public PrintersPage()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}