using InvestCloud.TestMM.Service.Helper;
using Moq;

namespace InvestCloud.TestMM.UnitTests.Helper;

public class MatrixOperationsTest
{
    private readonly Mock<MatrixOperations> MockMatrixOperations;

    public MatrixOperationsTest()
    {
        MockMatrixOperations = new Mock<MatrixOperations>();
    }

    [Fact]
    public void GenerateMD5_True()
    {
        var testValue = "0-111";
        var md5HashForTest = "280C89E5338F7973850521CADDB320D1";
        var actual = MockMatrixOperations.Object.GenerateMD5(testValue);

        Assert.Equal(md5HashForTest, actual);
    }

    [Fact]
    public void GenerateMD5_False()
    {
        var testValue = "0-112"; // Incorrect Value.
        var md5HashForTest = "280C89E5338F7973850521CADDB320D1";
        var actual = MockMatrixOperations.Object.GenerateMD5(testValue);

        Assert.NotEqual(md5HashForTest, actual);
    }

}