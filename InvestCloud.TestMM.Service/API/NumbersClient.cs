using InvestCloud.TestMM.Service.Models;
using System.Text.Json;
using System.Text;

namespace InvestCloud.TestMM.Service.API;

public class NumbersClient
{
    private readonly HttpClient _client;

    public NumbersClient()
    {
        _client = new HttpClient();
    }

    public async Task<bool> InitializeData(int size)
    {
        var url = "https://recruitment-test.investcloud.com/api/numbers/init/";
        var response = await _client.GetAsync($@"{url}{size}").ConfigureAwait(false);
        var result = JsonSerializer.Deserialize<NumberDto>(await response.Content.ReadAsStringAsync());

        if (result != null && result.Success) return true;
        return false;
    }

    public async Task<string> Validate(string md5HashedString)
    {
        var url = "https://recruitment-test.investcloud.com/api/numbers/validate";
        var requestContent = new StringContent(md5HashedString, Encoding.UTF8, "application/json");
        var response = _client.PostAsync(url, requestContent).Result;
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<ValidateDto>(await response.Content.ReadAsStringAsync());

        if (result != null) return result.Value;
        return "FAILED TO VALIDATE !!!";
    }

    public async Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type, int size)
    {
        var url = "https://recruitment-test.investcloud.com/api/numbers/";
        var listOfNumbers = Enumerable.Range(0, size).ToArray();
        var tasks = listOfNumbers.Select(async index =>
        {
            var response = await _client.GetAsync($@"{url}{dataSet}/{type}/{index}").ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<NumberArrayDto>(await response.Content.ReadAsStringAsync());
            return result;
        });

        NumberArrayDto?[] res = await Task.WhenAll(tasks);
        List<NumberArrayDto?> result = res.Where(r => true).ToList();
        return result;
    }

}