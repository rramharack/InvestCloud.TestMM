using InvestCloud.TestMM.Application.Models;

namespace InvestCloud.TestMM.Application.Interfaces;

public interface IMatrixOperations
{
    public int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB);

    public string GenerateMD5(string concatenatedString);

    public MatricesData GetMultiplyMatricesData(int size);
}