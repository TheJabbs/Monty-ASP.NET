using api.core.Dtos.Shared;
using api.core.Dtos.Transaction;
using api.core.Models;
using api.Models;

namespace api.core.Interfaces.IRepository;

public interface ITransactionRepo
{
    Task<List<GetTransactionDto>?> GetAllTransactions();
    Task<GetTransactionDto?> GetTransactionById(int id);
    Task<ResponseDto<GetTransactionDto>> CreateTransaction(Transaction transaction);
    Task<bool> DeleteTransaction(int id);
    Task<GetFullTransactionInfoDto?> GetFullTransactionInfo(int transactionId);
}