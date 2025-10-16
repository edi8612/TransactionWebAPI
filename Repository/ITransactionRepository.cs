using TransactionWebAPI.Models;

namespace TransactionWebAPI.Repository
{
    public interface ITransactionRepository
    {

        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<Transaction> GetTransactionAsync(int id);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
    }
}
