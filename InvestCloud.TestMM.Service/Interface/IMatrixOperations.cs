using InvestCloud.TestMM.Service.Models;

namespace InvestCloud.TestMM.Service.Interface;

public interface IMatrixOperations
{
    public int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB);

    public string GenerateMD5(string concatenatedString);

    public MatricesData GetMultiplyMatricesData(int size);
}