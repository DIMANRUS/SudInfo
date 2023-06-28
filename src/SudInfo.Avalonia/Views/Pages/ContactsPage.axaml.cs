namespace SudInfo.Avalonia.Views.Pages;

public partial class ContactsPage : ReactiveUserControl<ContactsPageViewModel>
{
    public ContactsPage()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}