namespace api.core.Dtos.Transaction;
using api.core.Models;


public class GetTransactionDto
{
    public int TransactionId { get; set; }
    public DateTime TransactionDate { get; set; } 
    public decimal Amount { get; set; }
    public decimal Fees { get; set; }
    public string SenderAccountId { get; set; } = "";
    public string ReceiverAccountId { get; set; } = "";
    public Occasion Occasion { get; set; }

}