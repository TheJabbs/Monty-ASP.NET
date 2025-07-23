using System.ComponentModel.DataAnnotations;

namespace api.core.Dtos.Customer;

public class PostCustomerDto
{
    public string CustFullName { get; set; } = "";
    
    [Required]
    public DateTime Dob { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = "";
    
    [Required]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string Phone { get; set; } = "";
    
    [Required]
    [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
    public string Address { get; set; } = "";
    
    [Required]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
    public string Password { get; set; } = "";
}