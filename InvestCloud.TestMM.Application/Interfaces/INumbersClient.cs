using InvestCloud.TestMM.Application.Models;

namespace InvestCloud.TestMM.Application.Interfaces;

public interface INumbersClient
{
    public Task<bool> InitializeData(int size);

    public Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_ArraySize(string dataSet, string type, int arraySize);

    public Task<string> Validate(string md5HashedString);
}