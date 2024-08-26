namespace InvestCloud.TestMM.Service.Models;

public class MatricesData
{
    public MatricesData(int[,] matrixA, int[,] matrixB)
    {
        MatrixA = matrixA;
        MatrixB = matrixB;
    }

    public int[,] MatrixA { get; set; }

    public int[,] MatrixB { get; set; }
}