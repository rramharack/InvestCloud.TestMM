namespace InvestCloud.TestMM.Service.Models;

public class NumberDto : ResponseBase
{
    public NumberDto(int value, string cause, bool success) : base(cause, success)
    {
        Value = value;
        this.Cause = cause;
        this.Success = success;
    }

    public int Value { get; set; }

}