namespace SudInfo.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        //var suspension = new AutoSuspendHelper(ApplicationLifetime!);
        //RxApp.SuspensionHost.CreateNewAppState = () => new MainWindow();
        //suspension.OnFrameworkInitializationCompleted();

        //Locator.CurrentMutable.RegisterConstant<IScreen>(RxApp.SuspensionHost.GetAppState<MainWindowViewModel>());
        //Locator.CurrentMutable.Register<IViewFor<ComputersPageViewModel>>(() => new ComputersPage());

        //new MainWindow { DataContext = Locator.Current.GetService<IScreen>() }.Show();

        //base.OnFrameworkInitializationCompleted();
        Locator.CurrentMutable.RegisterConstant<IScreen>(new MainWindowViewModel());
        Locator.CurrentMutable.Register<IViewFor<ComputersPageViewModel>>(() => new ComputersPage());

        // Получаем корневую модель представления и инициализируем контекст данных.
        new MainWindow { DataContext = Locator.Current.GetService<IScreen>() }.Show();
        base.OnFrameworkInitializationCompleted();

        //if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        //{
        //    //desktop.MainWindow = new MainWindow
        //    //{
        //    //    DataContext = new MainWindowViewModel(),
        //    //};
        //    new MainWindow().Show();
        //}

        //base.OnFrameworkInitializationCompleted();
    }
}