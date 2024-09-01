using InvestCloud.TestMM.Application.Helper;
using InvestCloud.TestMM.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace InvestCloud.TestMM.UnitTests.Helper;
public class MatrixOperationsTest
{
    private readonly Mock<ILogger<MatrixOperations>> _mockLogger;
    private readonly Mock<INumbersClient> _mockINumbersClient;

    public MatrixOperationsTest()
    {
        _mockLogger = new Mock<ILogger<MatrixOperations>>();
        _mockINumbersClient = new Mock<INumbersClient>();
    }

    [Fact]
    public void GenerateMD5_Size_2_True()
    {
        // TEST CASE (Small sub-set of data)
        // https://recruitment-test.investcloud.com/api/numbers/init/2
        //int[,] matrixA = { { 1, 0 }, { 0, -1 } };
        //int[,] matrixB = { { 0, -1 }, { -1, -1 } };

        var testValue = "0-111";
        var md5HashForTest = "280C89E5338F7973850521CADDB320D1";
        var matrixOperations = new MatrixOperations(_mockLogger.Object, _mockINumbersClient.Object);
        var actual = matrixOperations.GenerateMD5(testValue);

        Assert.Equal(md5HashForTest, actual);
    }

    [Fact]
    public void GenerateMD5_Size_2_False()
    {
        var testValue = "0-112"; // Incorrect Value.
        var md5HashForTest = "280C89E5338F7973850521CADDB320D1";
        var matrixOperations = new MatrixOperations(_mockLogger.Object, _mockINumbersClient.Object);
        var actual = matrixOperations.GenerateMD5(testValue);

        Assert.NotEqual(md5HashForTest, actual);
    }

    [Fact]
    public void GenerateMD5_Size_3_True()
    {
        // TEST CASE (Small sub-set of data)
        // https://recruitment-test.investcloud.com/api/numbers/init/3
        //int[,] matrixA = { { 0, -2, -2 }, { -2, -2, 0 }, { -2, 0, 1 } };
        //int[,] matrixB = { { -2, -1, 0 }, { -1, 0, 2 }, { 0, 2, 2 } };

        var testValue = "2-4-862-4442";
        var md5HashForTest = "31696A68123C1C338B4B8F73C187BAF1";
        var matrixOperations = new MatrixOperations(_mockLogger.Object, _mockINumbersClient.Object);
        var actual = matrixOperations.GenerateMD5(testValue);

        Assert.Equal(md5HashForTest, actual);
    }

    [Fact]
    public void GenerateMD5_Size_3_False()
    {
        var testValue = "2-4-862-4443"; // Incorrect Value.
        var md5HashForTest = "280C89E5338F7973850521CADDB320D1";
        var matrixOperations = new MatrixOperations(_mockLogger.Object, _mockINumbersClient.Object);
        var actual = matrixOperations.GenerateMD5(testValue);

        Assert.NotEqual(md5HashForTest, actual);
    }
}