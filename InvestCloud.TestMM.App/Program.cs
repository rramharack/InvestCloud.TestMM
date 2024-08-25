using System.Diagnostics;
using System.Runtime.CompilerServices;
using InvestCloud.TestMM.Service.API;
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
        var IMatrixOperations = services.GetRequiredService<IMatrixOperations>();
        var IPrintMatrix = services.GetRequiredService<IPrintMatrix>();
        var INumbersClient = services.GetRequiredService<INumbersClient>();

        var timer = new Stopwatch();
        timer.Start();

        int size = 3;

        Console.WriteLine($"Starting...  Size: {size}");
        Console.WriteLine();

        if (!INumbersClient.InitializeData(size).Result) throw new Exception("ERROR: Cannot Initialize Data !!!");
        //var listForMatrixA = client.RetrievesCollectionBy_DataSet_Type_Index("A", "row", size).Result;
        //var listForMatrixB = client.RetrievesCollectionBy_DataSet_Type_Index("B", "row", size).Result;

        //Declare and initialize two two-dimensional arrays, X and Y.
        int[,] matrixA = new int[size, size];
        int[,] matrixB = new int[size, size];

        //// Loop through the arrays X and Y and add the corresponding element.
        //for (int i = 0; i < listForMatrixA.Count; i++)
        //{
        //    for (int j = 0; j < listForMatrixA[i].Value.Length; j++)
        //        matrixA[i, j] = listForMatrixA[i].Value[j];
        //}

        //// Loop through the arrays X and Y and add the corresponding element.
        //for (int i = 0; i < listForMatrixB.Count; i++)
        //{
        //    for (int j = 0; j < listForMatrixB[i].Value.Length; j++)
        //        matrixB[i, j] = listForMatrixB[i].Value[j];
        //}

        // TEST CASE (Small sub-set of data)
        // https://recruitment-test.investcloud.com/api/numbers/init/2
        //int[,] matrixA = { { 1, 0 }, { 0, -1 } };
        //int[,] matrixB = { { 0, -1 }, { -1, -1 } };

        //// https://recruitment-test.investcloud.com/api/numbers/init/3
        //int[,] matrixA = { { 0, -2, -2 }, { -2, -2, 0 }, { -2, 0, 1 } };
        //int[,] matrixB = { { -2, -1, 0 }, { -1, 0, 2 }, { 0, 2, 2 } };

        int[,] matrixC = IMatrixOperations.MultiplyMatrices(matrixA, matrixB);

        ////Display the elements of the array [TESTING].
        IPrintMatrix.Print2DimensionalArray("matrixA", matrixA);
        IPrintMatrix.Print2DimensionalArray("matrixB", matrixB);
        IPrintMatrix.Print2DimensionalArray("matrixC", matrixC);

        var concatenatedString = string.Join("", matrixC.Cast<int>());

        //Console.WriteLine("Concatenated String:");
        //Console.WriteLine(concatenatedString);
        //Console.WriteLine();

        var md5Hash = IMatrixOperations.GenerateMD5(concatenatedString);

        timer.Stop();

        TimeSpan timeTaken = timer.Elapsed;
        Console.WriteLine($"Finished !! Time taken: {timeTaken:m\\:ss\\.fff}");

        Console.WriteLine();
        Console.WriteLine("md5 Hash:");
        Console.WriteLine(md5Hash);
        Console.WriteLine();

        Console.WriteLine("Validating:");
        //var msg = client.Validate(md5Hash).Result;
        //Console.WriteLine(msg);
        Console.WriteLine();
    }

    private static ServiceProvider CreateServices()
    {
        //setup our DI
        var serviceProvider = new ServiceCollection()
            .AddLogging(options =>
            {
                options.ClearProviders();
                options.AddConsole();
            })
            .AddSingleton<IMatrixOperations, MatrixOperations>()
            .AddSingleton<IPrintMatrix, PrintMatrix>()
            .AddSingleton<INumbersClient, NumbersClient>()
            .BuildServiceProvider();

  
        //configure console logging
        serviceProvider.GetService<ILoggerFactory>();

        var logger = serviceProvider.GetService<ILoggerFactory>()!.CreateLogger<Program>();
        logger.LogDebug("Starting InvestCloud.TestMM.App");

        return serviceProvider;
    }

}