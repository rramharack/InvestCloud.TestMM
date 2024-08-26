using InvestCloud.TestMM.Service.Interface;

namespace InvestCloud.TestMM.Service.Models;
public abstract class BasePrint : IPrintMatrix
{
    /// <summary>
    /// Implemented, which would override the default implementation
    /// </summary>
    /// <param name="name"></param>
    /// <param name="matrix"></param>
    public virtual void Print2DimensionalArray(string name, int[,] matrix)
    {
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

    public abstract void Display2DimensionalArray(int[,] matrixA, int[,] matrixB, int[,] matrixC, string concatenatedString);

    public abstract void DisplayCompletedTimeAndMd5Hash(TimeSpan timeCompleted, string MD5Hash);
}