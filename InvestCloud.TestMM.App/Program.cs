using System.Diagnostics;
using InvestCloud.TestMM.Service.Common;
using InvestCloud.TestMM.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace InvestCloud.TestMM.App;

internal class Program
{
    private static readonly int _size = Service.Common.App.Settings.DatasetSize;

    static void Main(string[] args)
    {
        try
        {
            var services = StartUp.CreateServices();

            var IMatrixOperations = services.GetService<IMatrixOperations>();
            var IPrintMatrix = services.GetService<IPrintMatrix>();
            var INumbersClient = services.GetService<INumbersClient>();
            var INumbersClient_Alt = services.GetService<INumbersClient_Alt>();

            // Check if created successfully...
            var matrixOperations = IMatrixOperations ?? throw new ArgumentNullException(nameof(IMatrixOperations));
            var printMatrix = IPrintMatrix ?? throw new ArgumentNullException(nameof(IPrintMatrix));
            var numbersClient = INumbersClient ?? throw new ArgumentNullException(nameof(INumbersClient));
            var numbersClient_Alt = INumbersClient_Alt ?? throw new ArgumentNullException(nameof(INumbersClient_Alt));

            var timer = new Stopwatch();
            timer.Start();

            if (!INumbersClient.InitializeData(_size).Result) //         *** Uses RestSharp  *** 
                //if (!INumbersClient_Alt.InitializeData(size).Result)  *** Uses HttpClient *** 
                throw new Exception("ERROR: Cannot Initialize Data !!!");

            var matricesData = IMatrixOperations?.GetMultiplyMatricesData(_size) ??
                               throw new ArgumentNullException($"IMatrixOperations?.GetMultiplyMatricesData({_size})");

            int[,] matrixC = IMatrixOperations?.MultiplyMatrices(matricesData.MatrixA, matricesData.MatrixB);
            var concatenatedString = string.Join("", matrixC.Cast<int>());

            // Display the elements of the array [TESTING].
            if (_size < 4)
                IPrintMatrix?.Display2DimensionalArray(matricesData.MatrixA, matricesData.MatrixB, matrixC, concatenatedString);

            var md5Hash = IMatrixOperations?.GenerateMD5(concatenatedString);
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            IPrintMatrix?.DisplayCompletedTimeAndMd5Hash(timeTaken, md5Hash);

            Console.WriteLine("Validating:");
            var msg = INumbersClient.Validate(md5Hash).Result;
            Console.WriteLine(msg);
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: " + Helper.GetFullMessage(e));
        }
    }
}