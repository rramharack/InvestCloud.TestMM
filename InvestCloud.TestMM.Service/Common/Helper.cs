namespace InvestCloud.TestMM.Service.Common;

public static class Helper
{
    public static string GetFullMessage(Exception data)
    {
        return data.InnerException == null
            ? data.Message
            : data.Message + " --> " + data.InnerException.Message;
    }

    public static T[,] ToTwoDimensionalArray<T>(this IEnumerable<IEnumerable<T>> enumerable)
    {
        var lines = enumerable.Select(inner => inner.ToArray()).ToArray();
        var columnCount = lines.Max(columns => columns.Length);
        var tda = new T[lines.Length, columnCount];
        for (var lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            var line = lines[lineIndex];
            for (var columnIndex = 0; columnIndex < line.Length; columnIndex++)
            {
                tda[lineIndex, columnIndex] = line[columnIndex];
            }
        }
        return tda;
    }
}