namespace SudInfo.Avalonia.Views.Pages;
public partial class UsersPage : ReactiveUserControl<UsersPageViewModel>
{
    public UsersPage()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}