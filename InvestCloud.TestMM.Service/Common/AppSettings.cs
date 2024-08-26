namespace InvestCloud.TestMM.Service.Common;

public class App
{
    public static AppSettings Settings { get; set; }

    static App() { Settings = new AppSettings(); }
}

public class AppSettings : AppConfig
{
    // System 
    // TODO: NOT IN USE !!!
    // Can be set up in public void ConfigureServices(IServiceCollection services)
    // Example: 
    //App.Settings.AppName = Convert.ToString(Configuration["Application"]); ;
    //App.Settings.AppEnv = Environment.EnvironmentName;
    //App.Settings.AppVersion = Convert.ToString(Configuration["version"]);
    public string AppEnv { get; set; }
    public string AppName { get; set; }
    public string AppVersion { get; set; }
}