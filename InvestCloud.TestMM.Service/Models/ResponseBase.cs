namespace InvestCloud.TestMM.Service.Models;

public abstract class ResponseBase
{
    protected ResponseBase(string cause, bool success)
    {
        Cause = cause;
        Success = success;
    }

    public string Cause { get; set; }
    public bool Success { get; set; }
}