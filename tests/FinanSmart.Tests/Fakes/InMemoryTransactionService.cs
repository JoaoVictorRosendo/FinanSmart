using FinanSmart.Models;
using FinanSmart.Services;

namespace FinanSmart.Tests.Fakes;

public class InMemoryTransactionService : ITransactionService
{
    private readonly List<Transaction> _transactions = new();

    public IReadOnlyList<Transaction> GetTransactions()
        => _transactions.OrderByDescending(t => t.Date).ToList();

    public void AddTransaction(Transaction transaction)
        => _transactions.Add(transaction);

    public void DeleteTransaction(Guid id)
        => _transactions.RemoveAll(t => t.Id == id);

    public decimal GetBalance()
        => _transactions.Sum(t => t.Type == TransactionType.Income ? t.Amount : -t.Amount);

    public decimal GetTotalIncome()
        => _transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);

    public decimal GetTotalExpenses()
        => _transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
}
