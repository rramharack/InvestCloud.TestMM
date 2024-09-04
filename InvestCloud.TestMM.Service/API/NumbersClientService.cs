using RestSharp;

namespace InvestCloud.TestMM.Service.API;

public class NumbersClientService
{
    #region RestSharp

    private readonly IRestClient _client;

    public NumbersClientService(IRestClient client)
    {
        _client = client;
    }

    public async Task<string?> InitializeData(int size, string initializeDataUrl)
    {
        var request = new RestRequest(initializeDataUrl + $"{size}");
        var response = await _client.ExecuteGetAsync(request);
        return response.Content;
    }

    public async Task<List<string?>> RetrievesCollectionBy_DataSet_Type_Index(string url, int arraySize, int batchSize)
    {
        var result = new List<string?>();
        int numberOfBatches = (int)Math.Ceiling((double)arraySize / batchSize);
        var listOfNumbers = Enumerable.Range(0, arraySize).ToArray();

        for (int i = 0; i < numberOfBatches; i++)
        {
            var currentBatchIds = listOfNumbers.Skip(i * batchSize).Take(batchSize);
            var tasks = currentBatchIds.Select(async index =>
            {
                var request = new RestRequest($"{url}/{index}");
                var response = await _client.ExecuteGetAsync(request);
                return response.Content;
            });

            string?[] res = await Task.WhenAll(tasks);
            result = res.Where(r => true).ToList();
        }
        return result;
    }

    public async Task<string?> Validate(string url, string md5HashedString)
    {
        var request = new RestRequest(url, Method.Post);
        request.AddBody(md5HashedString);
        var result = await _client.PostAsync(request);
        return result.Content;
    }

    #endregion RestSharp
}