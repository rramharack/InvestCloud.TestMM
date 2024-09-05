using InvestCloud.TestMM.Application.Common;
using InvestCloud.TestMM.Application.Interfaces;
using InvestCloud.TestMM.Application.Models;
using InvestCloud.TestMM.Service.API;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Text.Json;

namespace InvestCloud.TestMM.Application.Concrete;

public class NumbersClient : INumbersClient
{
    private readonly ILogger<NumbersClient> _logger;
    private readonly NumbersClientService _numbersClientService;

    public NumbersClient(ILogger<NumbersClient> logger, IRestClient restClient)
    {
        _numbersClientService = new NumbersClientService(restClient);
        _logger = logger;
    }

    public async Task<bool> InitializeData(int size)
    {
        _logger.LogInformation("Initialize Data: " + App.Settings.App + $"\nSize: {size}\n");
        var result = await _numbersClientService.InitializeData(size, App.Settings.InitializeData);
        var deserializeResult = JsonSerializer.Deserialize<NumberDto>(result);
        return deserializeResult is { Success: true };
    }

    public async Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_ArraySize(string dataSet, string type, int arraySize)
    {
        var url = App.Settings.GetDataByValues + $"{dataSet}/{type}";
        _logger.LogInformation($"Retrieving (RestSharp) for Matrix{dataSet}: " + $"URL==> {url}\n");
        var result = await _numbersClientService.RetrievesCollectionBy_DataSet_Type_Index(url, arraySize, App.Settings.BatchSize);

        return result.SelectMany(x => x).ToList().Select(item => JsonSerializer.Deserialize<NumberArrayDto>(item)).ToList();
    }

    public async Task<string> Validate(string md5HashedString)
    {
        var result = await _numbersClientService.Validate(App.Settings.Validate, md5HashedString);
        var deserializeResult = JsonSerializer.Deserialize<ValidateDto>(result);
        return deserializeResult != null ? deserializeResult.Value : App.Settings.VALIDATE_FAILED;
    }
}