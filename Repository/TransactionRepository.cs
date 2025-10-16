using Microsoft.EntityFrameworkCore;
using TransactionWebAPI.Data;
using TransactionWebAPI.Models;

namespace TransactionWebAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly ApplicationDbContext _db;
        public TransactionRepository(ApplicationDbContext db)
        {

            _db = db;
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            await _db.Transactions.AddAsync(transaction);
            await _db.SaveChangesAsync();
            return transaction;
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _db.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _db.Transactions.Remove(transaction);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            return await _db.Transactions.FindAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            var transactions = await _db.Transactions.Include(t => t.Category).ToListAsync();
            return transactions;

        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            _db.Transactions.Update(transaction);
            await _db.SaveChangesAsync();
            return transaction;
        }
    }
}
