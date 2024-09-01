namespace InvestCloud.TestMM.Application.Interfaces;

public interface IPrintMatrix
{
    public void Print2DimensionalArray(string name, int[,] matrix);

    public void Display2DimensionalArray(int[,] matrixA, int[,] matrixB, int[,] matrixC, string concatenatedString);

    public void DisplayCompletedTimeAndMd5Hash(TimeSpan timeCompleted, string MD5Hash);
}