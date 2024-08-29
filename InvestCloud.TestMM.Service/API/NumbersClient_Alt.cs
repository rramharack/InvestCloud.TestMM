using InvestCloud.TestMM.Service.Interface;
using InvestCloud.TestMM.Service.Models;
using InvestCloud.TestMM.Service.Common;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text;

namespace InvestCloud.TestMM.Service.API;

public class NumbersClient_Alt : INumbersClient
{
    #region HttpClient

    private readonly HttpClient _client;
    private readonly ILogger<NumbersClient_Alt> _logger;

    public NumbersClient_Alt(ILogger<NumbersClient_Alt> logger)
    {
        _client = new HttpClient();
        _logger = logger;
    }

    public async Task<bool> InitializeData(int size)
    {
        _logger.LogInformation("Starting " + App.Settings.App + $"\nSize: {size}\n");
        var response = await _client.GetAsync(App.Settings.InitializeData + $"{size}").ConfigureAwait(false);
        var result = JsonSerializer.Deserialize<NumberDto>(await response.Content.ReadAsStringAsync());

        return result is { Success: true };
    }

    public async Task<List<List<NumberArrayDto?>>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type, int size)
    {
        var result = new List<NumberArrayDto?>();
        var resultList = new List<List<NumberArrayDto?>>();

        var batchSize = App.Settings.BatchSize;
        int numberOfBatches = (int)Math.Ceiling((double)size / batchSize);

        for (int i = 0; i < numberOfBatches; i++)
        {
            var listOfNumbers = Enumerable.Range(0, size).ToArray();
            var currentIds = listOfNumbers.Skip(i * batchSize).Take(batchSize);

            var tasks = currentIds.Select(async index =>
            {
                var response = await _client.GetAsync(App.Settings.GetDataByValues + $"{dataSet}/{type}/{index}").ConfigureAwait(false);
                var result = JsonSerializer.Deserialize<NumberArrayDto>(await response.Content.ReadAsStringAsync());
                return result;
            });

            NumberArrayDto?[] res = await Task.WhenAll(tasks);
            result = res.Where(r => true).ToList();
            resultList.Add(result);
        }

         return resultList;
    }

    public async Task<string> Validate(string md5HashedString)
    {
        var requestContent = new StringContent(md5HashedString, Encoding.UTF8, "application/json");
        var response = _client.PostAsync(App.Settings.Validate, requestContent).Result;
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<ValidateDto>(await response.Content.ReadAsStringAsync());
        return result != null ? result.Value : App.Settings.VALIDATE_FAILED;
    }

    #endregion HttpClient
}