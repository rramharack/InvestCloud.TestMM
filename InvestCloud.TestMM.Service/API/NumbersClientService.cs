﻿using RestSharp;

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
        var restRequest = new RestRequest(initializeDataUrl + $"{size}");
        var request = await _client.ExecuteGetAsync(restRequest);
        return request.Content;
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
                var restRequest = new RestRequest($"{url}/{index}");
                var request = await _client.GetAsync(restRequest);
                return request.Content;
            });

            string?[] res = await Task.WhenAll(tasks);
            List<string>  result = res.Where(r => true).ToList();
            resultList.Add(result);
        }

        return resultList;
    }

    public async Task<string?> Validate(string url, string md5HashedString)
    {
        var restRequest = new RestRequest(url, Method.Post);
        restRequest.AddBody(md5HashedString);
        var request = await _client.PostAsync(restRequest);
        return request.Content;
    }

    #endregion RestSharp
}