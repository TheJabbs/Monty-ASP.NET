using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.core.Models;

namespace api.Models;

public enum AccountCurrency
{
    Usd,
    Eur,
    Ll
}

public class Account
{
    [Key]
    [MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public string AccountNumber { get; set; } = "";


    [Required]
    public AccountCurrency Currency { get; set; }  // Enum as int by default

    [Required]
    [Column(TypeName = "decimal(18,3)")]
    public decimal Balance { get; set; }

    [Required]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [Required]
    public int CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Transaction>? SentTransactions { get; set; }
    public virtual ICollection<Transaction>? ReceivedTransactions { get; set; }
}