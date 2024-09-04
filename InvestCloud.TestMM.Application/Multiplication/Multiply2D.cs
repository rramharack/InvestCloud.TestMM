using System.Diagnostics;
using InvestCloud.TestMM.Application.Interfaces;

namespace InvestCloud.TestMM.Application.Multiplication;

public class Multiply2D : IMultiply2D
{
    private readonly INumbersClient _iNumbersClient;
    private readonly IMatrixOperations _iMatrixOperations;
    private readonly IPrintMatrix _iPrintMatrix;

    private static readonly int Size = Common.App.Settings.DatasetSize;
    private static readonly int PrintSize = Common.App.Settings.PrintSize;

    public Multiply2D(INumbersClient iNumbersClient, IMatrixOperations iMatrixOperations,
                      IPrintMatrix iPrintMatrix)
    {
        _iNumbersClient = iNumbersClient;
        _iMatrixOperations = iMatrixOperations;
        _iPrintMatrix = iPrintMatrix;
    }

    public async Task<string> GetComputation()
    {
        var timer = new Stopwatch();
        timer.Start();

        //if (!INumbersClient.InitializeData(_size).Result) //  *** Uses RestSharp (TO USE: Update CreateServices()) *** 
        if (!_iNumbersClient.InitializeData(Size).Result)   //  *** Uses HttpClient *** 
            throw new Exception("ERROR: Cannot Initialize Data !!!");

        var matricesData = _iMatrixOperations.GetMultiplyMatricesData(Size) ??
                           throw new ArgumentNullException($"_iMatrixOperations.GetMultiplyMatricesData({Size})");

        int[,] matrixC = _iMatrixOperations.MultiplyMatrices(matricesData.MatrixA, matricesData.MatrixB);
        var concatenatedString = string.Join("", matrixC.Cast<int>());

        // Display the elements of the array [TESTING].
        if (Size < PrintSize)
            _iPrintMatrix.Display2DimensionalArray(matricesData.MatrixA, matricesData.MatrixB, matrixC, concatenatedString);

        var md5Hash = _iMatrixOperations.GenerateMD5(concatenatedString);
        timer.Stop();

        TimeSpan timeTaken = timer.Elapsed;
        _iPrintMatrix.DisplayCompletedTimeAndMd5Hash(timeTaken, md5Hash);

        Console.WriteLine(@"Validating:");
        var msg = await _iNumbersClient.Validate(md5Hash);

        return msg;
    }
}