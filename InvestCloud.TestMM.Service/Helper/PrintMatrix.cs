using InvestCloud.TestMM.Service.Interface;
using Microsoft.Extensions.Logging;

namespace InvestCloud.TestMM.Service.Helper;

public class PrintMatrix : IPrintMatrix
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
    public void Print2DimensionalArray(string name, int[,] matrix)
    {
        try
        {
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

}