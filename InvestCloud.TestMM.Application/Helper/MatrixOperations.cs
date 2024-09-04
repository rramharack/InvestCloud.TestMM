using System.Security.Cryptography;
using System.Text;
using InvestCloud.TestMM.Application.Common.Enum;
using InvestCloud.TestMM.Application.Interfaces;
using InvestCloud.TestMM.Application.Models;
using Microsoft.Extensions.Logging;

namespace InvestCloud.TestMM.Application.Helper;

public class MatrixOperations : IMatrixOperations
{
    private readonly ILogger<MatrixOperations> _logger;
    private readonly INumbersClient _iNumbersClient;

    public MatrixOperations(ILogger<MatrixOperations> logger, INumbersClient iNumbersClient)
    {
        _logger = logger;
        _iNumbersClient = iNumbersClient;
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

    public MatricesData GetMultiplyMatricesData(int size)
    {
        var listForMatrixA = _iNumbersClient.RetrievesCollectionBy_DataSet_Type_ArraySize(DataSetEnum.A.ToString(), TypeEnum.row.ToString(), size).Result;
        var listForMatrixB = _iNumbersClient.RetrievesCollectionBy_DataSet_Type_ArraySize(DataSetEnum.B.ToString(), TypeEnum.row.ToString(), size).Result;

        //Declare and initialize two two-dimensional arrays, X and Y.
        int[,] matrixA = new int[size, size];
        int[,] matrixB = new int[size, size];

        // Loop through the arrays X and Y and add the corresponding element for matrixA
        var index = 0;
        foreach (var item in listForMatrixA.Select(c => c))
        {
            for (int j = 0; j < size; j++)
                matrixA[index, j] = item.Value[j];
            index++;
        }

        // Loop through the arrays X and Y and add the corresponding element for matrixB
        index = 0;
        foreach (var item in listForMatrixB.Select(c => c))
        {
            for (int j = 0; j < size; j++)
                matrixB[index, j] = item.Value[j];
            index++; 
        }

        return new MatricesData(matrixA, matrixB);
    }
}