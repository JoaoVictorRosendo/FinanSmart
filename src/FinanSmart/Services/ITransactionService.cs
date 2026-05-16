using FinanSmart.Models;

namespace FinanSmart.Services;

public interface ITransactionService
{
    IReadOnlyList<Transaction> GetTransactions();
    void AddTransaction(Transaction transaction);
    void DeleteTransaction(Guid id);
    decimal GetBalance();
    decimal GetTotalIncome();
    decimal GetTotalExpenses();
}
