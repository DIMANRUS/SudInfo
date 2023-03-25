using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SudInfo.Avalonia.Views.Pages;

public partial class ContactsPage : ReactiveUserControl<ContactsPageViewModel>
{
    public ContactsPage()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}