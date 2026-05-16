namespace FinanSmart.Models;

public class DashboardViewModel
{
    public decimal Balance { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public IReadOnlyList<Transaction> RecentTransactions { get; set; } = new List<Transaction>();
    public AwesomeApiResponse? Rates { get; set; }
}

public class TransactionsViewModel
{
    public IReadOnlyList<Transaction> Transactions { get; set; } = new List<Transaction>();
    public string? Error { get; set; }
}

public class NewTransactionForm
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; } = TransactionType.Expense;
    public string Category { get; set; } = "Outros";
    public DateTime Date { get; set; } = DateTime.Today;
}
