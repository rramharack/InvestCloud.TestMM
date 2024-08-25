using System.Security.Cryptography;
using System.Text;
using InvestCloud.TestMM.Service.Interface;
using Microsoft.Extensions.Logging;

namespace InvestCloud.TestMM.Service.Helper;

public class MatrixOperations : IMatrixOperations
{
    private readonly ILogger<MatrixOperations> _logger;

    public MatrixOperations(ILogger<MatrixOperations> logger)
    {
        _logger = logger;
    }

    [Obsolete]
    public int[,] MultiplyMatrices_v1(int[,] matrixA, int[,] matrixB)
    {
        try
        {
            var n = matrixA.GetLength(0);
            int[,]? result = new int[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                    result[i, j] += Multiply_MatrixA_Row_To_MatrixB_Column(matrixA, i, j, n, matrixB);
            }

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return new int[0, 0];
    }

    public int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
    {
        try
        {
            var n = matrixA.GetLength(0);
            int[,] result = new int[n, n];

            Parallel.For(0, matrixA.GetLength(0), row =>
            {
                for (var i = 0; i < matrixB.GetLength(0); i++)
                {
                    result[row, i] += Multiply_MatrixA_Row_To_MatrixB_Column(matrixA, row, i, n, matrixB);
                }
            });

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return new int[0, 0];
    }

    private int Multiply_MatrixA_Row_To_MatrixB_Column(int[,] matrixA, int currentIndex, int nextValue, int sizeOfMatrix, int[,] matrixB)
    {
        var matrixCValue = 0;
        for (var i = 0; i < sizeOfMatrix; i++)
            matrixCValue += matrixA[currentIndex, i] * matrixB[i, nextValue];

        return matrixCValue;
    }

    public string GenerateMD5(string concatenatedString)
    {
        return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(concatenatedString)).Select(s => s.ToString("X2")));
    }
}