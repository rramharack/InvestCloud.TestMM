namespace InvestCloud.TestMM.Service.Common;

public static class Helper
{
    public static string GetFullMessage(Exception data)
    {
        return data.InnerException == null
            ? data.Message
            : data.Message + " --> " + data.InnerException.Message;
    }
}