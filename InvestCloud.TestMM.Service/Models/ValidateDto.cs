﻿namespace InvestCloud.TestMM.Service.Models;

public class ValidateDto : ResponseBase
{
    public ValidateDto(string value, string cause, bool success) : base(cause, success)
    {
        Value = value;
        this.Cause = cause;
        this.Success = success;
    }

    public string Value { get; set; }
    public string Cause { get; set; }
    public bool Success { get; set; }
}