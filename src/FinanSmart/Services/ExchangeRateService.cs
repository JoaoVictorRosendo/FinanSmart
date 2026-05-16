using System.Net.Http.Json;
using FinanSmart.Models;

namespace FinanSmart.Services;

public class ExchangeRateService : IExchangeRateService
{
    private const string ApiUrl = "https://economia.awesomeapi.com.br/json/last/USD-BRL,EUR-BRL";
    private readonly HttpClient _http;

    public ExchangeRateService(HttpClient http)
    {
        _http = http;
    }

    public async Task<AwesomeApiResponse?> GetRatesAsync()
    {
        var response = await _http.GetFromJsonAsync<AwesomeApiResponse>(ApiUrl);
        return response;
    }
}
