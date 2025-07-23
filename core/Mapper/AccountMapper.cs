using api.core.Dtos.Account;
using api.Models;

namespace api.core.Mapper;

public class AccountMapper 
{
    public static Account PostAccountDtoToAccount(PostAccountDto postAccountDto)
    {
        return new Account
        {
            Currency = postAccountDto.Currency,
            CustomerId = postAccountDto.CustomerId
        };
    }
    
    public static GetAccountDto AccountToGetAccountDto(Account account)
    {
        return new GetAccountDto
        {
            AccountNumber = account.AccountNumber,
            Currency = account.Currency,
            Balance = account.Balance,
            Created = account.Created,
            CustomerId = account.CustomerId
        };
    }
    
    public static Account UpdateAccountDtoToAccount(UpdateAccountDto updateAccountDto, Account account)
    {
        account.AccountNumber = updateAccountDto.AccountNumber;
        account.Balance += updateAccountDto.TopUp;
        return account;
    }
    
    
    
}