using TransactionWebAPI.Models.Dto;

namespace TransactionWebAPI.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllAsync();
        Task<TransactionDTO> GetAsync(int id);
        Task<TransactionDTO> CreateAsync(TransactionCreateDTO dto);
        Task<TransactionDTO> UpdateAsync(TransactionUpdateDTO dto);
        Task DeleteAsync(int id);
    }
}
