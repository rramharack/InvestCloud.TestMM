using InvestCloud.TestMM.Service.Models;
using Microsoft.Extensions.Logging;

namespace InvestCloud.TestMM.Service.Helper;
public class PrintMatrix : BasePrint
{
    private readonly ILogger<PrintMatrix> _logger;

    public PrintMatrix(ILogger<PrintMatrix> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Prints a presentation of a two-dimensional array in an X and Y format for TESTING to the Console
    /// </summary>
    /// <param name="name">Name of Array</param>
    /// <param name="matrix">two-dimensional array</param>
    public override void Print2DimensionalArray(string name, int[,] matrix)
    {
        try
        {
            //base.Print2DimensionalArray(name, matrix); return;
            Console.Write($"{name}:");
            Console.WriteLine();
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }

    public override void Display2DimensionalArray(int[,] matrixA, int[,] matrixB, int[,] matrixC, string concatenatedString)
    {
        Print2DimensionalArray("matrixA", matrixA);
        Print2DimensionalArray("matrixB", matrixB);
        Print2DimensionalArray("matrixC", matrixC);

        Console.WriteLine("Concatenated String:");
        Console.WriteLine(concatenatedString);
        Console.WriteLine();
    }

    public override void DisplayCompletedTimeAndMd5Hash(TimeSpan timeTaken, string md5Hash)
    {
        Console.WriteLine($"Finished !! Time taken: {timeTaken:m\\:ss\\.fff}");

        Console.WriteLine();
        Console.WriteLine("md5 Hash:");
        Console.WriteLine(md5Hash);
        Console.WriteLine();
    }
}