using api.Models;

namespace api.core.Dtos.Account;

public class GetAccountDto
{
    public string AccountNumber { get; set; } = "";
    
    public AccountCurrency Currency { get; set; }
    
    public decimal Balance { get; set; }

    public DateTime Created { get; set; }
    
    public int CustomerId { get; set; }
}