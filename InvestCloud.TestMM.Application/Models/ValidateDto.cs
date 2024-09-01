namespace InvestCloud.TestMM.Application.Models;

public class ValidateDto : ResponseBase
{
    public ValidateDto(string value, string cause, bool success) : base(cause, success)
    {
        Value = value;
        this.Cause = cause;
        this.Success = success;
    }

    public string Value { get; set; }
}