using AutoMapper;
using TransactionWebAPI.Models;
using TransactionWebAPI.Models.Dto;
using TransactionWebAPI.Repository;

namespace TransactionWebAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;

        private readonly IMapper _mapper;


        public TransactionService(ITransactionRepository transactionRepo, IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _mapper = mapper;
        }

        public async Task<TransactionDTO> CreateAsync(TransactionCreateDTO dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);

            await _transactionRepo.CreateTransactionAsync(transaction);

            return _mapper.Map<TransactionDTO>(transaction);

        }

        public async Task DeleteAsync(int id)
        {
            await _transactionRepo.DeleteTransactionAsync(id);
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {
            var transactions = await _transactionRepo.GetTransactionsAsync();
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public async Task<TransactionDTO> GetAsync(int id)
        {
            var transaction = await _transactionRepo.GetTransactionAsync(id);
            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<TransactionDTO> UpdateAsync(TransactionUpdateDTO dto)
        {
            var transaction = await _transactionRepo.GetTransactionAsync(dto.Id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Category with ID {dto.Id} not found.");
            }

            _mapper.Map(dto, transaction);
            await _transactionRepo.UpdateTransactionAsync(transaction);

            return _mapper.Map<TransactionDTO>(transaction);
        }
    }
}
