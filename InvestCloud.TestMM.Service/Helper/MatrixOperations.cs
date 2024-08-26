﻿using System.Security.Cryptography;
using System.Text;
using InvestCloud.TestMM.Service.Common.Enum;
using InvestCloud.TestMM.Service.Interface;
using InvestCloud.TestMM.Service.Models;
using Microsoft.Extensions.Logging;

namespace InvestCloud.TestMM.Service.Helper;

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
        var listForMatrixA = _iNumbersClient.RetrievesCollectionBy_DataSet_Type_Index(DataSetEnum.A.ToString(), TypeEnum.row.ToString(), size).Result;
        var listForMatrixB = _iNumbersClient.RetrievesCollectionBy_DataSet_Type_Index(DataSetEnum.B.ToString(), TypeEnum.row.ToString(), size).Result;

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

        return new MatricesData(matrixA, matrixB);
    }
}