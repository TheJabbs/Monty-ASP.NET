using api.core.Dtos.Account;
using api.core.Dtos.Shared;
using api.Models;

namespace api.core.Interfaces.IRepository;

public interface IAccountRepo
{
    Task<ResponseDto<GetAccountDto>> CreateAccountAsync(Account account);
    Task<decimal> GetAccountBalanceAsync(string accountNumber);
    Task<ResponseDto<GetAccountDto>> UpdateAccountBalanceAsync(Account account);
    Task<bool> DeleteAccountAsync(Account accountNumber);
    Task<Account?> GetAccountAsync(string accountNumber);
    Task<List<Account>?> GetAllAccountsAsync();
}