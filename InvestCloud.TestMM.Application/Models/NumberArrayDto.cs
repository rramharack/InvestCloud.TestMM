namespace InvestCloud.TestMM.Application.Models;

public class NumberArrayDto : ResponseBase
{
    public NumberArrayDto(int[] value, string cause, bool success) : base(cause, success)
    {
        Value = value;
        this.Cause = cause;
        this.Success = success;
    }

    public int[] Value { get; set; }
}