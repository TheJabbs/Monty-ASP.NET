using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustId { get; set; }

    [Required]
    [MaxLength(255)]
    public string CustFullName { get; set; } = "";

    [Required]
    public DateTime Dob { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; } = "";

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = "";

    [Required]
    [MaxLength(255)]
    public string Address { get; set; } = "";

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = "";

    public virtual ICollection<Account>? Accounts { get; set; }
}