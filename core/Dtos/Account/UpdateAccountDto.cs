using System.ComponentModel.DataAnnotations;

namespace api.core.Dtos.Account;

public class UpdateAccountDto
{
    [Required] public string AccountNumber { get; set; } = "";
 
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Balance must be greater than zero.")]
    public decimal TopUp { get; set; }
}