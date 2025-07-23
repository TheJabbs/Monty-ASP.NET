using api.core.Dtos.Account;
using api.core.Dtos.Transaction;
using api.core.Models;

namespace api.core.Mapper;

public class TransactionMapper
{
    public static Transaction PostTransactionMapper(PostTransactionDto transactionDto)
    {
        return new Transaction
        {
            Amount = transactionDto.Amount,
            SenderAccountId = transactionDto.SenderAccountId,
            ReceiverAccountId = transactionDto.ReceiverAccountId,
            Occasion = transactionDto.Occasion,
        };
    }
    
    public static GetTransactionDto GetTransactionMapper(Transaction transaction)
    {
        return new GetTransactionDto
        {
            TransactionId = transaction.TransactionId,
            TransactionDate = transaction.TransactionDate,
            Amount = transaction.Amount,
            Fees = transaction.Fees,
            SenderAccountId = transaction.SenderAccountId,
            ReceiverAccountId = transaction.ReceiverAccountId,
            Occasion = transaction.Occasion
        };
    }
    
    public static Transaction GetTransactionDtoToTransactionMapper(GetTransactionDto transactionDto)
    {
        return new Transaction
        {
            TransactionId = transactionDto.TransactionId,
            TransactionDate = transactionDto.TransactionDate,
            Amount = transactionDto.Amount,
            Fees = transactionDto.Fees,
            SenderAccountId = transactionDto.SenderAccountId,
            ReceiverAccountId = transactionDto.ReceiverAccountId,
            Occasion = transactionDto.Occasion
        };
    }

    public static Transaction PostTransactionMapperToTransaction(PostTransactionDto transactionDto)
    {
        return new Transaction
        {
            Amount = transactionDto.Amount,
            SenderAccountId = transactionDto.SenderAccountId,
            ReceiverAccountId = transactionDto.ReceiverAccountId,
            Occasion = transactionDto.Occasion,
            Fees = 0 
        };
    }

    public static GetFullTransactionInfoDto? GetFullTransactionInfoMapper(Transaction transaction)
    {
        if (transaction == null || transaction.SenderAccount == null || transaction.ReceiverAccount == null)
        {
            return null;
        }

        return new GetFullTransactionInfoDto
        {
            SenderAccount = AccountMapper.AccountToGetAccountDto(transaction.SenderAccount),
            ReceiverAccount = AccountMapper.AccountToGetAccountDto(transaction.ReceiverAccount),
            Transaction = GetTransactionMapper(transaction)
        };
    }

}