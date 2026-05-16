using FinanSmart.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanSmart.Controllers;

public class CambioController : Controller
{
    private readonly IExchangeRateService _exchange;
    private readonly ITransactionService _transactions;

    public CambioController(IExchangeRateService exchange, ITransactionService transactions)
    {
        _exchange = exchange;
        _transactions = transactions;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var rates = await _exchange.GetRatesAsync();
            ViewBag.Balance = _transactions.GetBalance();
            return View(rates);
        }
        catch
        {
            ViewBag.Error = "Não foi possível buscar as cotações. Verifique sua conexão.";
            return View(null);
        }
    }
}
