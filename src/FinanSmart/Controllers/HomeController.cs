using FinanSmart.Models;
using FinanSmart.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanSmart.Controllers;

public class HomeController : Controller
{
    private readonly ITransactionService _transactions;
    private readonly IExchangeRateService _exchange;

    public HomeController(ITransactionService transactions, IExchangeRateService exchange)
    {
        _transactions = transactions;
        _exchange = exchange;
    }

    public async Task<IActionResult> Index()
    {
        AwesomeApiResponse? rates = null;
        try { rates = await _exchange.GetRatesAsync(); } catch { }

        var vm = new DashboardViewModel
        {
            Balance = _transactions.GetBalance(),
            TotalIncome = _transactions.GetTotalIncome(),
            TotalExpenses = _transactions.GetTotalExpenses(),
            RecentTransactions = _transactions.GetTransactions().Take(5).ToList(),
            Rates = rates
        };
        return View(vm);
    }

    public IActionResult Transacoes()
    {
        var vm = new TransactionsViewModel
        {
            Transactions = _transactions.GetTransactions()
        };
        return View(vm);
    }

    [HttpPost]
    public IActionResult Adicionar(NewTransactionForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Description) || form.Amount <= 0)
        {
            TempData["Error"] = "Preencha todos os campos corretamente.";
            return RedirectToAction(nameof(Transacoes));
        }

        _transactions.AddTransaction(new Transaction
        {
            Description = form.Description.Trim(),
            Amount = form.Amount,
            Type = form.Type,
            Category = form.Category,
            Date = form.Date
        });

        return RedirectToAction(nameof(Transacoes));
    }

    [HttpPost]
    public IActionResult Excluir(Guid id)
    {
        _transactions.DeleteTransaction(id);
        return RedirectToAction(nameof(Transacoes));
    }
}
