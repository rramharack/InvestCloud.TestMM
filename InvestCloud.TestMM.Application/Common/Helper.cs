namespace InvestCloud.TestMM.Application.Common;

public static class Helper
{
    public static string GetFullMessage(this Exception data)
    {
        return data.InnerException == null
            ? data.Message
            : data.Message + " --> " + data.InnerException.Message;
    }
}