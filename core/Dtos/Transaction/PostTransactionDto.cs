using System.ComponentModel.DataAnnotations;

namespace api.core.Dtos.Transaction;
using api.core.Models;


public class PostTransactionDto
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [Required] public string SenderAccountId { get; set; } = "";
    [Required] public string ReceiverAccountId { get; set; } = "";
    [Required]
    public Occasion Occasion { get; set; }

}