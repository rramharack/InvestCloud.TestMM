﻿using InvestCloud.TestMM.Application.Concrete;
using InvestCloud.TestMM.Application.Helper;
using InvestCloud.TestMM.Application.Interfaces;
using InvestCloud.TestMM.Application.Multiplication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
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
            .AddTransient<IMultiply2D, Multiply2D>()

            // TO USE: RestSharp *** IMPORTANT:: Comment out HttpClient below !!! 
            //.AddSingleton<IRestClient, RestClient>()
            //.AddTransient<INumbersClient, NumbersClient>()

            // TO USE: HttpClient
            .AddTransient<INumbersClient, NumbersClient_Alt>()
            .AddHttpClient()
            .RemoveAll<IHttpMessageHandlerBuilderFilter>()

            .BuildServiceProvider();

        // Configure Logging
        serviceProvider.GetService<ILoggerFactory>();
        return serviceProvider;
    }
}