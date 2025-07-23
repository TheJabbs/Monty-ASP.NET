using api.core.Dtos.Account;
using api.core.Service;
using api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace api.core.Controller;

[Route("api/Account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    
    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync([FromBody] PostAccountDto accountDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _accountService.CreateAccountAsync(accountDto);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAccountAsync([FromBody] UpdateAccountDto accountDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _accountService.UpdateAccountAsync(accountDto);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response.Message);
    }
    
    [HttpGet("{accountNumber}")]
    public async Task<IActionResult> GetAccountAsync(string accountNumber)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            return BadRequest("Account number is required");
        }

        var response = await _accountService.GetAccountAsync(accountNumber);
        if (response == null)
        {
            return NotFound("Account not found");
        }

        return Ok(response);
    }
    
    [HttpGet("balance/{accountNumber}")]
    public async Task<IActionResult> GetAccountBalanceAsync(string accountNumber)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            return BadRequest("Account number is required");
        }

        try
        {
            var balance = await _accountService.GetAccountBalanceAsync(accountNumber);
            return Ok(new { Balance = balance });
        }
        catch (NotFound ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("{accountNumber}")]
    public async Task<IActionResult> DeleteAccountAsync(string accountNumber)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            return BadRequest("Account number is required");
        }

        try
        {
            var result = await _accountService.DeleteAccountAsync(accountNumber);
            if (!result)
            {
                return NotFound("Account not found");
            }
            return Ok("Account deleted successfully");
        }
        catch (NotFound ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        var accounts = await _accountService.GetAllAccounts();
        if (accounts == null || !accounts.Any())
        {
            return NotFound("No accounts found");
        }

        return Ok(accounts);
    }

}