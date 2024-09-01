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
        var result = await _client.GetAsync<string>(initializeDataUrl + $"{size}");
        return result;
    }

    //public async Task<List<List<NumberArrayDto?>>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type, int size)
    //{
    //    var resultList = new List<List<NumberArrayDto?>>();
    //    var batchSize = App.Settings.BatchSize;
    //    int numberOfBatches = (int)Math.Ceiling((double)size / batchSize);
    //    var listOfNumbers = Enumerable.Range(0, size).ToArray();

    //    for (int i = 0; i < numberOfBatches; i++)
    //    {
    //        var tasks = listOfNumbers.Select(async index =>
    //        {
    //            var result = await _client.GetAsync<NumberArrayDto>(App.Settings.GetDataByValues + $"{dataSet}/{type}/{index}");
    //            return result;
    //        });

    //        NumberArrayDto?[] res = await Task.WhenAll(tasks);
    //        List<NumberArrayDto?> result = res.Where(r => true).ToList();
    //        resultList.Add(result);
    //    }
    //    return resultList;
    //}

    //public async Task<string> Validate(string md5HashedString)
    //{
    //    var request = new RestRequest(App.Settings.Validate, Method.Post);
    //    var result = await _client.PostAsync<ValidateDto>(request);
    //    return result != null ? result.Value : App.Settings.VALIDATE_FAILED;
    //}

    #endregion RestSharp
}