using InvestCloud.TestMM.Service.Models;
using InvestCloud.TestMM.Service.Interface;
using InvestCloud.TestMM.Service.Common;
using RestSharp;
using Microsoft.Extensions.Logging;

namespace InvestCloud.TestMM.Service.API;

public class NumbersClient : INumbersClient
{
    #region RestSharp

    private readonly IRestClient _client;
    private readonly ILogger<NumbersClient> _logger;

    public NumbersClient(IRestClient client, ILogger<NumbersClient> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<bool> InitializeData(int size)
    {
        _logger.LogInformation("Starting " + App.Settings.App + $"\nSize: {size}\n");
        var result = await _client.GetAsync<NumberDto>(App.Settings.InitializeData + $"{size}");
        return result is { Success: true };
    }

    public async Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type, int size)
    {
        var listOfNumbers = Enumerable.Range(0, size).ToArray();
        var tasks = listOfNumbers.Select(async index =>
        {
            var result = await _client.GetAsync<NumberArrayDto>(App.Settings.GetDataByValues + $"{dataSet}/{type}/{index}");
            return result;
        });

        NumberArrayDto?[] res = await Task.WhenAll(tasks);
        List<NumberArrayDto?> result = res.Where(r => true).ToList();
        return result;
    }

    public async Task<string> Validate(string md5HashedString)
    {
        var request = new RestRequest(App.Settings.Validate, Method.Post);
        var result = await _client.PostAsync<ValidateDto>(request);
        return result != null ? result.Value : App.Settings.VALIDATE_FAILED;
    }

    #endregion RestSharp
}