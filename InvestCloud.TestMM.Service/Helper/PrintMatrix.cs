namespace InvestCloud.TestMM.Service.Helper;

public class PrintMatrix
{
    /// <summary>
    /// Prints a presentation of a two-dimensional array in an X and Y format for TESTING to the Console
    /// </summary>
    /// <param name="name">Name of Array</param>
    /// <param name="matrix">two-dimensional array</param>
    public static void Print2DimensionalArray(string name, int[,] matrix)
    {
        Console.WriteLine($"{name}:");
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
}