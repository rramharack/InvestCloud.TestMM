using InvestCloud.TestMM.Application.Common;
using InvestCloud.TestMM.Application.Interfaces;
using InvestCloud.TestMM.Application.Models;
using InvestCloud.TestMM.Service.API;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace InvestCloud.TestMM.Application.Concrete;

public class NumbersClient : INumbersClient
{
    private readonly ILogger<NumbersClient> _logger;
    private readonly NumbersClientService _NumbersClientService;

    public NumbersClient(ILogger<NumbersClient> logger, IRestClient httpClient)
    {
        _NumbersClientService = new NumbersClientService(httpClient);
        _logger = logger;
    }


    public Task<bool> InitializeData(int size)
    {
        //TODO: 
        //_logger.LogInformation("Starting " + App.Settings.App + $"\nSize: {size}\n");
        //var result = await _client.GetAsync<NumberDto>(App.Settings.InitializeData + $"{size}");
        //return result is { Success: true };
        return Task.FromResult(true);
    }

    public async Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type,
        int size)
    {
        ////var resultList = new List<List<NumberArrayDto?>>();
        ////var batchSize = App.Settings.BatchSize;
        ////int numberOfBatches = (int)Math.Ceiling((double)size / batchSize);
        ////var listOfNumbers = Enumerable.Range(0, size).ToArray();

        ////for (int i = 0; i < numberOfBatches; i++)
        ////{
        ////    var tasks = listOfNumbers.Select(async index =>
        ////    {
        ////        var result = await _client.GetAsync<NumberArrayDto>(App.Settings.GetDataByValues + $"{dataSet}/{type}/{index}");
        ////        return result;
        ////    });

        ////    NumberArrayDto?[] res = await Task.WhenAll(tasks);
        ////    List<NumberArrayDto?> result = res.Where(r => true).ToList();
        ////    resultList.Add(result);
        ////}
        ////return resultList;


        return null;
    }

    public Task<string> Validate(string md5HashedString)
    {
        //var request = new RestRequest(App.Settings.Validate, Method.Post);
        //var result = await _client.PostAsync<ValidateDto>(request);
        //return result != null ? result.Value : App.Settings.VALIDATE_FAILED;
        return Task.FromResult<string>("");
    }
}