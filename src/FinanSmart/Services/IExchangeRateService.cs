using FinanSmart.Models;

namespace FinanSmart.Services;

public interface IExchangeRateService
{
    Task<AwesomeApiResponse?> GetRatesAsync();
}
