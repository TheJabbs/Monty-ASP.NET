using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace api.core.Dtos.Customer;

public class UpdateCustomerDto
{
    [Required]
    public int CustId { get; set; }
    public string? CustFullName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? Phone { get; set; }
    public string? Address { get; set; }
    [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters long.")]
    public string? PasswordHash { get; set; }
}