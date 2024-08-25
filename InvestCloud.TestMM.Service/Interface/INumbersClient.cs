namespace InvestCloud.TestMM.Service.Interface;

public interface INumbersClient
{
    public Task<bool> InitializeData(int size);
}