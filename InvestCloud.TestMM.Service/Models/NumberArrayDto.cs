namespace InvestCloud.TestMM.Service.Models;

public class NumberArrayDto
{
    public NumberArrayDto(int[] value, string cause, bool success)
    {
        Value = value;
        Cause = cause;
        Success = success;
    }

    public int[] Value { get; set; }
    public string Cause { get; set; }
    public bool Success { get; set; }

}