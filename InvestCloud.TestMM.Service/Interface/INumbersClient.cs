using InvestCloud.TestMM.Service.Models;

namespace InvestCloud.TestMM.Service.Interface;

public interface INumbersClient
{
    public Task<bool> InitializeData(int size);

    public Task<List<List<NumberArrayDto?>>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type, int size);

    public Task<string> Validate(string md5HashedString);
}