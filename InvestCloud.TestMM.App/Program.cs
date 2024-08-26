using System.Diagnostics;
using InvestCloud.TestMM.Service.API;
using InvestCloud.TestMM.Service.Common.Enum;
using InvestCloud.TestMM.Service.Helper;
using InvestCloud.TestMM.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace InvestCloud.TestMM.App;

internal class Program
{
    static void Main(string[] args)
    {
        var services = CreateServices();

        var IMatrixOperations = services.GetService<IMatrixOperations>();
        var IPrintMatrix = services.GetService<IPrintMatrix>();
        var INumbersClient = services.GetService<INumbersClient>();
        var INumbersClient_Alt = services.GetService<INumbersClient_Alt>();

        int size = 4;

        var timer = new Stopwatch();
        timer.Start();

        if (!INumbersClient.InitializeData(size).Result) // *** Uses RestSharp *** 
            //if (!INumbersClient_Alt.InitializeData(size).Result) *** Uses HttpClient *** 
            throw new Exception("ERROR: Cannot Initialize Data !!!");

        var listForMatrixA = INumbersClient.RetrievesCollectionBy_DataSet_Type_Index(DataSetEnum.A.ToString(), TypeEnum.row.ToString(), size).Result;
        var listForMatrixB = INumbersClient.RetrievesCollectionBy_DataSet_Type_Index(DataSetEnum.B.ToString(), TypeEnum.row.ToString(), size).Result;

        //Declare and initialize two two-dimensional arrays, X and Y.
        int[,] matrixA = new int[size, size];
        int[,] matrixB = new int[size, size];

        // Loop through the arrays X and Y and add the corresponding element.
        for (int i = 0; i < listForMatrixA.Count; i++)
        {
            for (int j = 0; j < listForMatrixA[i].Value.Length; j++)
                matrixA[i, j] = listForMatrixA[i].Value[j];
        }

        // Loop through the arrays X and Y and add the corresponding element.
        for (int i = 0; i < listForMatrixB.Count; i++)
        {
            for (int j = 0; j < listForMatrixB[i].Value.Length; j++)
                matrixB[i, j] = listForMatrixB[i].Value[j];
        }

        // TEST CASE (Small sub-set of data)
        // https://recruitment-test.investcloud.com/api/numbers/init/2
        //int[,] matrixA = { { 1, 0 }, { 0, -1 } };
        //int[,] matrixB = { { 0, -1 }, { -1, -1 } };

        //// https://recruitment-test.investcloud.com/api/numbers/init/3
        //int[,] matrixA = { { 0, -2, -2 }, { -2, -2, 0 }, { -2, 0, 1 } };
        //int[,] matrixB = { { -2, -1, 0 }, { -1, 0, 2 }, { 0, 2, 2 } };

        int[,] matrixC = IMatrixOperations.MultiplyMatrices(matrixA, matrixB);
        var concatenatedString = string.Join("", matrixC.Cast<int>());

        // Display the elements of the array [TESTING].
        if (size < 4)
            IPrintMatrix?.Display2DimensionalArray(matrixA, matrixB, matrixC, concatenatedString);

        var md5Hash = IMatrixOperations.GenerateMD5(concatenatedString);
        timer.Stop();
        TimeSpan timeTaken = timer.Elapsed;
        IPrintMatrix?.DisplayCompletedTimeAndMd5Hash(timeTaken, md5Hash);

        Console.WriteLine("Validating:");
        var msg = INumbersClient.Validate(md5Hash).Result;
        Console.WriteLine(msg);
        Console.WriteLine();
    }

    private static ServiceProvider CreateServices()
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
            .AddSingleton<INumbersClient_Alt, NumbersClient_Alt>()
            .BuildServiceProvider();

        // Configure Logging
        serviceProvider.GetService<ILoggerFactory>();
        return serviceProvider;
    }
}