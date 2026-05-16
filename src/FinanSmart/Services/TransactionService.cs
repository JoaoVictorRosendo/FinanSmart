using System.Text.Json;
using FinanSmart.Models;

namespace FinanSmart.Services;

public class TransactionService : ITransactionService
{
    private readonly string _filePath;
    private List<Transaction> _transactions;
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public TransactionService(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, "data", "transactions.json");
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        _transactions = Load();
    }

    public IReadOnlyList<Transaction> GetTransactions()
        => _transactions.OrderByDescending(t => t.Date).ToList();

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        Save();
    }

    public void DeleteTransaction(Guid id)
    {
        _transactions.RemoveAll(t => t.Id == id);
        Save();
    }

    public decimal GetBalance()
        => _transactions.Sum(t => t.Type == TransactionType.Income ? t.Amount : -t.Amount);

    public decimal GetTotalIncome()
        => _transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);

    public decimal GetTotalExpenses()
        => _transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

    private List<Transaction> Load()
    {
        if (!File.Exists(_filePath)) return new List<Transaction>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
    }

    private void Save()
        => File.WriteAllText(_filePath, JsonSerializer.Serialize(_transactions, JsonOptions));
}
