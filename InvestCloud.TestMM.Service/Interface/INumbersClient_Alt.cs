using InvestCloud.TestMM.Service.Models;

namespace InvestCloud.TestMM.Service.Interface;
public interface INumbersClient_Alt
{
    public Task<bool> InitializeData(int size);

    public Task<List<NumberArrayDto?>> RetrievesCollectionBy_DataSet_Type_Index(string dataSet, string type, int size);

    public Task<string> Validate(string md5HashedString);
}