using api.core.Dtos.Account;

namespace api.core.Dtos.Transaction;

public class GetFullTransactionInfoDto
{
    public required GetAccountDto SenderAccount { get; set; }
    public required GetAccountDto ReceiverAccount { get; set; }
    public required GetTransactionDto Transaction { get; set; }
}