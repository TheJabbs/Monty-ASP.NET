using System.ComponentModel.DataAnnotations;
using api.Models;

namespace api.core.Dtos.Account;

public class PostAccountDto
{
    [Required]
    [EnumDataType(typeof(AccountCurrency))]
    public AccountCurrency Currency { get; set; }
    
    [Required]
    public int CustomerId { get; set; }
}