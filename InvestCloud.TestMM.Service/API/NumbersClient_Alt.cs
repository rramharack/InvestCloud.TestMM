using InvestCloud.TestMM.Service.Interface;
using InvestCloud.TestMM.Service.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text;

namespace InvestCloud.TestMM.Service.API;

public class NumbersClient_Alt : INumbersClient_Alt
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
        _logger.LogInformation($"Starting InvestCloud.TestMM.App\nSize: {size}\n");
        var url = $"https://recruitment-test.investcloud.com/api/numbers/init/{size}";
        var response = await _client.GetAsync(url).ConfigureAwait(false);
        var result = JsonSerializer.Deserialize<NumberDto>(await response.Content.ReadAsStringAsync());

        return result is { Success: true };
    }

    public async Task<string> Validate(string md5HashedString)
    {
        var url = "https://recruitment-test.investcloud.com/api/numbers/validate";
        var requestContent = new StringContent(md5HashedString, Encoding.UTF8, "application/json");
        var response = _client.PostAsync(url, requestContent).Result;
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<ValidateDto>(await response.Content.ReadAsStringAsync());
        return result != null ? result.Value : "FAILED TO VALIDATE !!!";
    }

    public async Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type, int size)
    {
        var url = "https://recruitment-test.investcloud.com/api/numbers/";
        var listOfNumbers = Enumerable.Range(0, size).ToArray();
        var tasks = listOfNumbers.Select(async index =>
        {
            var response = await _client.GetAsync($"{url}{dataSet}/{type}/{index}").ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<NumberArrayDto>(await response.Content.ReadAsStringAsync());
            return result;
        });

        NumberArrayDto?[] res = await Task.WhenAll(tasks);
        List<NumberArrayDto?> result = res.Where(r => true).ToList();
        return result;
    }

    #endregion HttpClient
}