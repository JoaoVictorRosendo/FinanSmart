using FinanSmart.Models;
using FinanSmart.Tests.Fakes;

namespace FinanSmart.Tests;

public class TransactionServiceTests
{
    private static InMemoryTransactionService CreateService() => new();

    [Fact]
    public void AddTransaction_ShouldIncreaseCount()
    {
        var service = CreateService();
        service.AddTransaction(new Transaction { Description = "Salário", Amount = 3000m, Type = TransactionType.Income, Category = "Salário" });

        Assert.Single(service.GetTransactions());
    }

    [Fact]
    public void GetBalance_ShouldReturnIncomeMinusExpenses()
    {
        var service = CreateService();
        service.AddTransaction(new Transaction { Description = "Salário", Amount = 5000m, Type = TransactionType.Income, Category = "Salário" });
        service.AddTransaction(new Transaction { Description = "Aluguel", Amount = 1500m, Type = TransactionType.Expense, Category = "Moradia" });

        Assert.Equal(3500m, service.GetBalance());
    }

    [Fact]
    public void GetBalance_WithNoTransactions_ShouldReturnZero()
    {
        var service = CreateService();
        Assert.Equal(0m, service.GetBalance());
    }

    [Fact]
    public void DeleteTransaction_ShouldRemoveIt()
    {
        var service = CreateService();
        var t = new Transaction { Description = "Teste", Amount = 100m, Type = TransactionType.Expense, Category = "Outros" };
        service.AddTransaction(t);
        service.DeleteTransaction(t.Id);

        Assert.Empty(service.GetTransactions());
    }

    [Fact]
    public void GetTotalIncome_ShouldSumOnlyIncomes()
    {
        var service = CreateService();
        service.AddTransaction(new Transaction { Description = "Salário", Amount = 4000m, Type = TransactionType.Income, Category = "Salário" });
        service.AddTransaction(new Transaction { Description = "Freelance", Amount = 1000m, Type = TransactionType.Income, Category = "Freelance" });
        service.AddTransaction(new Transaction { Description = "Mercado", Amount = 500m, Type = TransactionType.Expense, Category = "Alimentação" });

        Assert.Equal(5000m, service.GetTotalIncome());
    }

    [Fact]
    public void GetTotalExpenses_ShouldSumOnlyExpenses()
    {
        var service = CreateService();
        service.AddTransaction(new Transaction { Description = "Salário", Amount = 4000m, Type = TransactionType.Income, Category = "Salário" });
        service.AddTransaction(new Transaction { Description = "Mercado", Amount = 300m, Type = TransactionType.Expense, Category = "Alimentação" });
        service.AddTransaction(new Transaction { Description = "Aluguel", Amount = 1200m, Type = TransactionType.Expense, Category = "Moradia" });

        Assert.Equal(1500m, service.GetTotalExpenses());
    }

    [Fact]
    public void GetTransactions_ShouldReturnOrderedByDateDescending()
    {
        var service = CreateService();
        service.AddTransaction(new Transaction { Description = "A", Amount = 100m, Type = TransactionType.Income, Category = "Outros", Date = new DateTime(2025, 1, 1) });
        service.AddTransaction(new Transaction { Description = "B", Amount = 200m, Type = TransactionType.Income, Category = "Outros", Date = new DateTime(2025, 6, 1) });
        service.AddTransaction(new Transaction { Description = "C", Amount = 300m, Type = TransactionType.Income, Category = "Outros", Date = new DateTime(2025, 3, 1) });

        var result = service.GetTransactions();

        Assert.Equal("B", result[0].Description);
        Assert.Equal("C", result[1].Description);
        Assert.Equal("A", result[2].Description);
    }
}
