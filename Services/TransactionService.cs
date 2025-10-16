using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

            try
            {
                var transaction = _mapper.Map<Transaction>(dto);
                if (transaction.Value <= 0)
                {
                    throw new ArgumentException("Transaction amount must be greater than zero.");
                }
                if (string.IsNullOrWhiteSpace(transaction.Title))
                {
                    throw new ArgumentException("Transaction title cannot be empty.");
                }
                transaction.CreatedAt = DateTime.UtcNow;
                transaction.UpdatedAt = DateTime.UtcNow;


                await _transactionRepo.CreateTransactionAsync(transaction);

                return _mapper.Map<TransactionDTO>(transaction);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An unexpected error occurred while creating transaction.", ex);
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _transactionRepo.DeleteTransactionAsync(id);
                if (id <= 0)
                {
                    throw new ArgumentException("Invalid transaction ID.");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error deleting trasaction");
            }

        }

        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {


            try
            {
                var transactions = await _transactionRepo.GetTransactionsAsync();
                if (transactions == null || !transactions.Any())
                {
                    throw new ApplicationException("No transactions found.");
                }
                return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while retrieving transactions.", ex);
            }
        }

        public async Task<TransactionDTO> GetAsync(int id)
        {
            try
            {
                var transaction = await _transactionRepo.GetTransactionAsync(id);
                if (transaction == null)
                {
                    throw new KeyNotFoundException($"Transaction with ID {id} not found.");
                }
                return _mapper.Map<TransactionDTO>(transaction);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while retrieving the transaction.", ex);

            }
        }
        public async Task<TransactionDTO> UpdateAsync(TransactionUpdateDTO dto)
        {

            try
            {

                var transaction = await _transactionRepo.GetTransactionAsync(dto.Id);
                if (transaction == null)
                {
                    throw new KeyNotFoundException($"Transaction with ID {dto.Id} not found.");
                }

                _mapper.Map(dto, transaction);

                transaction.UpdatedAt = DateTime.UtcNow;

                await _transactionRepo.UpdateTransactionAsync(transaction);

                return _mapper.Map<TransactionDTO>(transaction);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while updating the transaction.", ex);
            }



        }
    }
}
