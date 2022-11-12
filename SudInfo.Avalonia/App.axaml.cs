using SudInfo.Avalonia.Extensions;

namespace SudInfo.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // DI Initialization
        ServiceCollectionExtension.Init();

        #region Locator Pages Initialization
        Locator.CurrentMutable.RegisterConstant<IScreen>(new MainWindowViewModel());
        Locator.CurrentMutable.Register<IViewFor<ComputersPageViewModel>>(() => new ComputersPage());
        Locator.CurrentMutable.Register<IViewFor<PrintersPageViewModel>>(() => new PrintersPage());
        #endregion

        MainWindow mainWindow = new() { DataContext = Locator.Current.GetService<IScreen>() };

        ServiceCollectionExtension.ServiceProvider.GetService<INavigationService>()?.SetWindow(mainWindow);
        ServiceCollectionExtension.ServiceProvider.GetService<IDialogService>()?.SetCurrentWindow(mainWindow);

        mainWindow.Show();
        base.OnFrameworkInitializationCompleted();
    }
}