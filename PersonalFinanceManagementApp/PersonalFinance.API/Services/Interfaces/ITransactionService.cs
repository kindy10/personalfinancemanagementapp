using PersonalFinance.Shared.DTOs.Transactions;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetAllAsync(Guid userId);
        Task<TransactionDto> CreateAsync(Guid userId, CreateTransactionRequestDto request);
        Task<bool> DeleteAsync(Guid id,  Guid userId);
    }
}
