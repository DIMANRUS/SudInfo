namespace SudInfo.Avalonia.Views.Pages;
public partial class PeripheryPage : ReactiveUserControl<PeripheryPageViewModel>
{
    public PeripheryPage()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}