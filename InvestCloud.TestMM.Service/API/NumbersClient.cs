using InvestCloud.TestMM.Service.Models;
using InvestCloud.TestMM.Service.Interface;
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
        _logger.LogInformation($"Starting InvestCloud.TestMM.App\nSize: {size}\n");
        var url = $"https://recruitment-test.investcloud.com/api/numbers/init/{size}";
        var result = await _client.GetAsync<NumberDto>($"{url}");
        return result is { Success: true };
    }

    public async Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type, int size)
    {
        var url = "https://recruitment-test.investcloud.com/api/numbers/";
        var listOfNumbers = Enumerable.Range(0, size).ToArray();
        var tasks = listOfNumbers.Select(async index =>
        {
            var result = await _client.GetAsync<NumberArrayDto>($"{url}{dataSet}/{type}/{index}");
            return result;
        });

        NumberArrayDto?[] res = await Task.WhenAll(tasks);
        List<NumberArrayDto?> result = res.Where(r => true).ToList();
        return result;
    }

    public async Task<string> Validate(string md5HashedString)
    {
        var url = "https://recruitment-test.investcloud.com/api/numbers/validate";
        var request = new RestRequest(url, Method.Post);
        var result = await _client.PostAsync<ValidateDto>(request);
        return result != null ? result.Value : "FAILED TO VALIDATE !!!";
    }

    #endregion RestSharp
}