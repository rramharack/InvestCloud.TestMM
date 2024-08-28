using InvestCloud.TestMM.Service.API;
using InvestCloud.TestMM.Service.Helper;
using InvestCloud.TestMM.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace InvestCloud.TestMM.App;

public class StartUp
{
    internal static ServiceProvider CreateServices()
    {
        // Setup DI
        var serviceProvider = new ServiceCollection()
            .AddLogging(options =>
            {
                options.ClearProviders();
                options.AddConsole();
            })
            .AddScoped<IMatrixOperations, MatrixOperations>()
            .AddScoped<IPrintMatrix, PrintMatrix>()
            .AddSingleton<IRestClient, RestClient>()
            .AddTransient<INumbersClient, NumbersClient>()
            .AddTransient<INumbersClient, NumbersClient_Alt>()
            .BuildServiceProvider();

        // Configure Logging
        serviceProvider.GetService<ILoggerFactory>();
        return serviceProvider;
    }
}