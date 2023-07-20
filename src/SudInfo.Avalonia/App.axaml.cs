namespace SudInfo.Avalonia;

public class App : Application
{
    #region Properties

    public static Window? MainWindow { get; private set; }

    #endregion

    #region Initialization

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public async override void OnFrameworkInitializationCompleted()
    {
        MainWindow mainWindow = new();

        if (!File.Exists("appsettings.json"))
        {
            var json = JsonSerializer.Serialize(new AppSettings());
            await File.WriteAllTextAsync("appsettings.json", json);
        }

        #region Load ThemeVariant

        var appsettingsJson = await File.ReadAllTextAsync("appsettings.json");
        AppSettings settings = JsonSerializer.Deserialize<AppSettings>(appsettingsJson);
        RequestedThemeVariant = settings.Theme switch
        {
            "Dark" => ThemeVariant.Dark,
            "Light" => ThemeVariant.Light,
            _ => ThemeVariant.Dark
        };
        if (settings.Theme == "Acrylic")
        {
            mainWindow.TransparencyLevelHint = new List<WindowTransparencyLevel>() {
                WindowTransparencyLevel.AcrylicBlur
            };
            mainWindow.Background = null;
        }

        #endregion

        #region Locator Pages Initialization

        Locator.CurrentMutable.RegisterConstant<IScreen>(new MainWindowViewModel());
        Locator.CurrentMutable.Register<IViewFor<ComputersPageViewModel>>(() => new ComputersPage());
        Locator.CurrentMutable.Register<IViewFor<PrintersPageViewModel>>(() => new PrintersPage());
        Locator.CurrentMutable.Register<IViewFor<MonitorsPageViewModel>>(() => new MonitorsPage());
        Locator.CurrentMutable.Register<IViewFor<UsersPageViewModel>>(() => new UsersPage());
        Locator.CurrentMutable.Register<IViewFor<RutokensPageViewModel>>(() => new RutokensPage());
        Locator.CurrentMutable.Register<IViewFor<PeripheryPageViewModel>>(() => new PeripheryPage());
        Locator.CurrentMutable.Register<IViewFor<WorkplacesPageViewModel>>(() => new WorkplacesPage());
        Locator.CurrentMutable.Register<IViewFor<ServersPageViewModel>>(() => new ServersPage());
        Locator.CurrentMutable.Register<IViewFor<TasksPageViewModel>>(() => new TasksPage());
        Locator.CurrentMutable.Register<IViewFor<PasswordsPageViewModel>>(() => new PasswordsPage());
        Locator.CurrentMutable.Register<IViewFor<AppsPageViewModel>>(() => new AppsPage());
        Locator.CurrentMutable.Register<IViewFor<ContactsPageViewModel>>(() => new ContactsPage());
        Locator.CurrentMutable.Register<IViewFor<CartridgesPageViewModel>>(() => new CartridgesPage());

        #endregion

        mainWindow.DataContext = DataContext = Locator.Current.GetService<IScreen>();

        ServiceCollectionExtension.ServiceProvider.GetService<NavigationService>()?.SetWindow(mainWindow);
        ServiceCollectionExtension.ServiceProvider.GetService<DialogService>()?.SetMainWindow(mainWindow);
        ServiceCollectionExtension.ServiceProvider.GetService<DialogService>()?.SetCurrentWindow(mainWindow);

        mainWindow.Show();
        MainWindow = mainWindow;
        base.OnFrameworkInitializationCompleted();
    }

    #endregion
}