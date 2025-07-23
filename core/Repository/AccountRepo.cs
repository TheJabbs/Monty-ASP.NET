using api.core.Dtos.Account;
using api.core.Dtos.Shared;
using api.core.Interfaces.IRepository;
using api.core.Mapper;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.core.Repository;

public class AccountRepo : IAccountRepo
{
    private readonly ApplicationDbContext _context;

    public AccountRepo(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<ResponseDto<GetAccountDto>> CreateAccountAsync(Account account)
    {
        try
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var acc = AccountMapper.AccountToGetAccountDto(account);
            return new ResponseDto<GetAccountDto>
            {
                Success = true,
                Message = "Account created successfully",
                Data = acc
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<GetAccountDto>
            {
                Success = false,
                Message = $"Error creating account: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<decimal> GetAccountBalanceAsync(string accountNumber)
    {
        return await _context.Accounts
            .Where(a => a.AccountNumber == accountNumber)
            .Select(a => a.Balance)
            .FirstOrDefaultAsync();
    }

    public async Task<ResponseDto<GetAccountDto>> UpdateAccountBalanceAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();

        var accDto = AccountMapper.AccountToGetAccountDto(account);
        return new ResponseDto<GetAccountDto>
        {
            Success = true,
            Message = "Account updated successfully",
            Data = accDto
        };
    }

    public Task<bool> DeleteAccountAsync(Account account)
    {
        try
        {
            _context.Accounts.Remove(account);
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }
        catch (Exception ex)
        {
            return Task.FromResult(false);
        }
    }

    public async Task<Account?> GetAccountAsync(string accountNumber)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number cannot be empty", nameof(accountNumber));

        return await _context.Accounts
            .Where(a => a.AccountNumber == accountNumber)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Account>?> GetAllAccountsAsync()
    {
        return await _context.Accounts.ToListAsync();
    }
}