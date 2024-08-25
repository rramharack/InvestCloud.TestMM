namespace InvestCloud.TestMM.Service.Interface;

public interface IMatrixOperations
{
    public int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB);

    public string GenerateMD5(string concatenatedString);
}