using Microsoft.Extensions.Configuration;

namespace InvestCloud.TestMM.Application.Common;

public class AppConfig
{
    private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT") ?? "PROD"}.json", optional: false, true)
        .Build();

    private static IConfigurationSection AppSettings;
    private static IConfigurationSection Messages;

    internal AppConfig()
    {
        AppSettings = Configuration.GetSection("AppSettings");
        Messages = Configuration.GetSection("Messages");
    }

    public string App => AppSettings.GetSection("App").Value;
    public int DatasetSize => int.Parse(AppSettings.GetSection("DatasetSize").Value);
    public int PrintSize => int.Parse(AppSettings.GetSection("PrintSize").Value);
    public int BatchSize => int.Parse(AppSettings.GetSection("BatchSize").Value);
    public string InitializeData => AppSettings.GetSection("InitializeData").Value;
    public string GetDataByValues => AppSettings.GetSection("GetDataByValues").Value;
    public string Validate => AppSettings.GetSection("Validate").Value;

    public string VALIDATE_FAILED => Messages.GetSection("VALIDATE_FAILED").Value;

}