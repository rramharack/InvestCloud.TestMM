using System.Security.Cryptography;
using System.Text;

namespace InvestCloud.TestMM.Service.Helper;

public class MatrixOperations
{
    [Obsolete]
    public static int[,] MultiplyMatrices_v1(int[,] matrixA, int[,] matrixB)
    {
        var n = matrixA.GetLength(0);
        int[,] result = new int[n, n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
                result[i, j] += Multiply_MatrixA_Row_To_MatrixB_Column(matrixA, i, j, n, matrixB);
        }

        return result;
    }

    public static int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
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

    private static int Multiply_MatrixA_Row_To_MatrixB_Column(int[,] matrixA, int currentIndex, int nextValue, int sizeOfMatrix, int[,] matrixB)
    {
        var matrixCValue = 0;
        for (var i = 0; i < sizeOfMatrix; i++)
            matrixCValue += matrixA[currentIndex, i] * matrixB[i, nextValue];

        return matrixCValue;
    }

    public static string GenerateMD5(string concatenatedString)
    {
        return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(concatenatedString)).Select(s => s.ToString("X2")));
    }
}