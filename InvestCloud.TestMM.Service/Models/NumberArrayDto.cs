namespace InvestCloud.TestMM.Service.Models;

public class NumberArrayDto : ResponseBase
{
    public NumberArrayDto(int[] value, string cause, bool success) : base(cause, success)
    {
        Value = value;
        this.Cause = cause;
        this.Success = success;
    }

    public int[] Value { get; set; }
    public string Cause { get; set; }
    public bool Success { get; set; }

}