using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.core.Models;

public enum Occasion
{
    Rent,
    Food,
    Shopping,
    Entertainment,
    Transportation,
    Other
}

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    [Required]
    [Column(TypeName = "decimal(18,3)")]
    public decimal Amount { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,3)")]
    public decimal Fees { get; set; }

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public string SenderAccountId { get; set; } = "";

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public string ReceiverAccountId { get; set; } = "";

    [Required]
    public Occasion Occasion { get; set; }

    [ForeignKey(nameof(SenderAccountId))]
    public virtual Account? SenderAccount { get; set; }

    [ForeignKey(nameof(ReceiverAccountId))]
    public virtual Account? ReceiverAccount { get; set; }
}