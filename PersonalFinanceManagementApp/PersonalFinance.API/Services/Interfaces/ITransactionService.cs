using PersonalFinance.Shared.DTOs.Transactions;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetAllAsync(Guid userId);
        Task<TransactionDto> CreateAsync(Guid userId, CreateTransactionRequestDto request);
        Task  DeleteAsync(Guid id,  Guid userId);
        Task<TransactionDto> UpdateAsync(Guid id, Guid userId, UpdateTransactionRequestDto request);
    }
}
