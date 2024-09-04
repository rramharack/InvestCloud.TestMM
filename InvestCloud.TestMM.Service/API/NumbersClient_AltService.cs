using System.Text;

namespace InvestCloud.TestMM.Service.API;

public class NumbersClient_AltService
{
    #region HttpClient

    private readonly HttpClient _httpClient;

    public NumbersClient_AltService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> InitializeData(int arraySize, string initializeDataUrl)
    {
        var request = await _httpClient.GetAsync(initializeDataUrl + $"{arraySize}").ConfigureAwait(false);
        var result = await request.Content.ReadAsStringAsync();
        return result;
    }

    public async Task<List<List<string>>> RetrievesCollectionBy_DataSet_Type_Index(string url, int arraySize, int batchSize)
    {
        var resultList = new List<List<string>>();
        int numberOfBatches = (int)Math.Ceiling((double)arraySize / batchSize);
        var listOfNumbers = Enumerable.Range(0, arraySize).ToArray();

        for (int i = 0; i < numberOfBatches; i++)
        {
            var currentBatchIds = listOfNumbers.Skip(i * batchSize).Take(batchSize);
            var tasks = currentBatchIds.Select(async index =>
            {
                var request = await _httpClient.GetAsync($"{url}/{index}").ConfigureAwait(false);
                var result = await request.Content.ReadAsStringAsync();
                return result;
            });

            string[] res = await Task.WhenAll(tasks);
            List<string> result = res.Where(r => true).ToList();
            resultList.Add(result);
        }

        return resultList;
    }

    public async Task<string> Validate(string url, string md5HashedString)
    {
        var requestContent = new StringContent(md5HashedString, Encoding.UTF8, "application/json");
        var request = await _httpClient.PostAsync(url, requestContent);
        request.EnsureSuccessStatusCode();

        var result = await request.Content.ReadAsStringAsync();
        return result;
    }

    #endregion HttpClient
}