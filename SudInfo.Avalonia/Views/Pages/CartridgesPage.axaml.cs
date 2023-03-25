using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SudInfo.Avalonia.Views.Pages;

public partial class CartridgesPage : ReactiveUserControl<CartridgesPageViewModel>
{
    public CartridgesPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}