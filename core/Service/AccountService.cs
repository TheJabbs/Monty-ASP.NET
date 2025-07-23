using api.core.Dtos.Account;
using api.core.Dtos.Shared;
using api.core.Mapper;
using api.core.Repository;
using api.Exceptions;
using api.Models;

namespace api.core.Service;

public class AccountService
{
    private readonly AccountRepo _accountRepo;
    
    public AccountService(AccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }
    
    public async Task<ResponseDto<GetAccountDto>> CreateAccountAsync(PostAccountDto accountDto)
    {
        var account = AccountMapper.PostAccountDtoToAccount(accountDto);
        Account? check;
        
        do
        {
            account.AccountNumber = IbanGenerate.GenerateIban(account.Currency.ToString());
             check = await _accountRepo.GetAccountAsync(account.AccountNumber); 
        }while(check != null);
        
        return await _accountRepo.CreateAccountAsync(account);
    }
    
    
    public async Task<ResponseDto<GetAccountDto>> UpdateAccountAsync(UpdateAccountDto accountDto)
    {
        var existingAccount = await _accountRepo.GetAccountAsync(accountDto.AccountNumber);
        if (existingAccount == null)
        {
            return new ResponseDto<GetAccountDto>()
            {
                Success = false,
                Message = "Account not found",
                Data = null
            };
        }

        var updatedAccount = AccountMapper.UpdateAccountDtoToAccount(accountDto, existingAccount);
        return await _accountRepo.UpdateAccountBalanceAsync(updatedAccount);
    }
    
    public async Task<decimal> GetAccountBalanceAsync(string accountNumber)
    {
        var balance = await _accountRepo.GetAccountBalanceAsync(accountNumber);
        if (balance < 0)
        {
            throw new NotFound("Account not found or balance is negative");
        }
        return balance;
    }
    
    public async Task<ResponseDto<GetAccountDto>> GetAccountAsync(string accountNumber)
    {
        var account = await _accountRepo.GetAccountAsync(accountNumber);
        if (account == null)
        {
            return new ResponseDto<GetAccountDto>()
            {
                Success = false,
                Message = "Account not found",
                Data = null
            };
        }
        
        var accountDto = AccountMapper.AccountToGetAccountDto(account);
        return new ResponseDto<GetAccountDto>()
        {
            Success = true,
            Message = "Account retrieved successfully",
            Data = accountDto
        };
    }
    
    public async Task<bool> DeleteAccountAsync(string accountNumber)
    {
        var check = await _accountRepo.GetAccountAsync(accountNumber);
        if (check == null)
        {
            throw new NotFound("Account not found");
        }
        return await _accountRepo.DeleteAccountAsync(check);
    }

    public async Task<List<GetAccountDto>?> GetAllAccounts()
    {
        var accounts = await _accountRepo.GetAllAccountsAsync();
        if (accounts == null || accounts.Count == 0)
        {
            return null;
        }
        
        return accounts.Select(AccountMapper.AccountToGetAccountDto).ToList();
    }
}