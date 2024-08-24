namespace InvestCloud.TestMM.Service.Models;
public class ValidateDto
{
    public ValidateDto(string value, string cause, bool success)
    {
        Value = value;
        Cause = cause;
        Success = success;
    }

    public string Value { get; set; }
    public string Cause { get; set; }
    public bool Success { get; set; }
}