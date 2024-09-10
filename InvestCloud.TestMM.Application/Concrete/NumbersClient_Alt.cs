using Microsoft.Extensions.Logging;
using InvestCloud.TestMM.Application.Common;
using InvestCloud.TestMM.Application.Interfaces;
using InvestCloud.TestMM.Application.Models;
using InvestCloud.TestMM.Service.API;
using System.Text.Json;

namespace InvestCloud.TestMM.Application.Concrete;

public class NumbersClient_Alt : INumbersClient
{
    #region HttpClient

    private readonly ILogger<NumbersClient_Alt> _logger;
    private readonly NumbersClient_AltService _numbersClientAltService;

    public NumbersClient_Alt(ILogger<NumbersClient_Alt> logger, HttpClient httpClient)
    {
        _numbersClientAltService = new NumbersClient_AltService(httpClient);
        _logger = logger;
    }

    public async Task<T> InitializeData<T>(int size)
    {
        _logger.LogInformation("Initialize Data: " + App.Settings.App + $"\nSize: {size}\n");
        var result = await _numbersClientAltService.InitializeData(size, App.Settings.InitializeData);
        var deserializeResult = JsonSerializer.Deserialize<NumberDto>(result);
        return (T)Convert.ChangeType(deserializeResult is { Success: true }, typeof(T));
    }

    public async Task<bool> InitializeData(int size)
    {
        _logger.LogInformation("Initialize Data: " + App.Settings.App + $"\nSize: {size}\n");
        var result  = await _numbersClientAltService.InitializeData(size, App.Settings.InitializeData);
        var deserializeResult = JsonSerializer.Deserialize<NumberDto>(result);
        return deserializeResult is { Success: true };
    }

    public async Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_ArraySize(string dataSet, string type, int arraySize)
    {
        var url = App.Settings.GetDataByValues + $"{dataSet}/{type}";
        _logger.LogInformation($"Retrieving (HttpClient) for Matrix{dataSet}: " + $"URL==> {url}\n");
        var result = await _numbersClientAltService.RetrievesCollectionBy_DataSet_Type_Index(url, arraySize, App.Settings.BatchSize);

        return result.SelectMany(x => x).ToList().Select(item => JsonSerializer.Deserialize<NumberArrayDto>(item)).ToList();
    }

    public async Task<string> Validate(string md5HashedString)
    {
        var result = await _numbersClientAltService.Validate(App.Settings.Validate, md5HashedString);
        var deserializeResult = JsonSerializer.Deserialize<ValidateDto>(result);
        return deserializeResult != null ? deserializeResult.Value : App.Settings.VALIDATE_FAILED;
    }

    #endregion HttpClient
}