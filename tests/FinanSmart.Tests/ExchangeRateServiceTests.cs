using System.Net;
using System.Text;
using FinanSmart.Services;
using Moq;
using Moq.Protected;

namespace FinanSmart.Tests;

public class ExchangeRateServiceTests
{
    private static HttpClient CreateMockedClient(string json)
    {
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });

        return new HttpClient(handler.Object);
    }

    [Fact]
    public async Task GetRatesAsync_ShouldReturnUsdAndEurRates()
    {
        const string json = """
        {
            "USDBRL": { "code":"USD","name":"Dólar","bid":"5.2500","ask":"5.2600","create_date":"2025-06-01 10:00:00" },
            "EURBRL": { "code":"EUR","name":"Euro","bid":"5.7000","ask":"5.7100","create_date":"2025-06-01 10:00:00" }
        }
        """;

        var service = new ExchangeRateService(CreateMockedClient(json));
        var result = await service.GetRatesAsync();

        Assert.NotNull(result);
        Assert.NotNull(result.UsdBrl);
        Assert.NotNull(result.EurBrl);
        Assert.Equal("5.2500", result.UsdBrl.Bid);
        Assert.Equal("5.7000", result.EurBrl.Bid);
    }

    [Fact]
    public async Task GetRatesAsync_BidValue_ShouldParseCorrectly()
    {
        const string json = """
        {
            "USDBRL": { "code":"USD","name":"Dólar","bid":"5.3142","ask":"5.3200","create_date":"2025-06-01 10:00:00" },
            "EURBRL": { "code":"EUR","name":"Euro","bid":"5.8900","ask":"5.9000","create_date":"2025-06-01 10:00:00" }
        }
        """;

        var service = new ExchangeRateService(CreateMockedClient(json));
        var result = await service.GetRatesAsync();

        Assert.Equal(5.3142m, result!.UsdBrl!.BidValue);
    }

    [Fact]
    public async Task GetRatesAsync_WhenApiReturnsError_ShouldThrow()
    {
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.ServiceUnavailable });

        var service = new ExchangeRateService(new HttpClient(handler.Object));

        await Assert.ThrowsAnyAsync<Exception>(() => service.GetRatesAsync());
    }
}
