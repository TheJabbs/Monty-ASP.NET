using api.core.Dtos.Account;
using api.core.Dtos.Shared;
using api.core.Dtos.Transaction;
using api.core.Mapper;
using api.core.Repository;
using api.utils;

namespace api.core.Service;

public class TransactionService
{
    private readonly TransactionRepo _transactionRepo;
    private readonly AccountService _accountService;

    public TransactionService(TransactionRepo transactionRepo, AccountService accountService)
    {
        _transactionRepo = transactionRepo;
        _accountService = accountService;
    }

    public Task<List<GetTransactionDto>?> GetAllTransactions()
    {
        return _transactionRepo.GetAllTransactions();
    }

    public Task<GetTransactionDto?> GetTransactionById(int id)
    {
        return _transactionRepo.GetTransactionById(id);
    }

public async Task<ResponseDto<GetTransactionDto>?> CreateTransaction(PostTransactionDto dto)
{
    var senderResponse = await _accountService.GetAccountAsync(dto.SenderAccountId);
    var sender = senderResponse.Data;

    if (sender == null)
    {
        return new ResponseDto<GetTransactionDto>
        {
            Success = false,
            Message = "Sender account not found",
            Data = null
        };
    }

    var receiverResponse = await _accountService.GetAccountAsync(dto.ReceiverAccountId);
    var receiver = receiverResponse.Data;

    if (receiver == null)
    {
        return new ResponseDto<GetTransactionDto>
        {
            Success = false,
            Message = "Receiver account not found",
            Data = null
        };
    }

    var fees = sender.Currency != receiver.Currency
        ? MoneyTransaction.CalculateFeeInSenderCurrency(dto.Amount)
        : 0m;

    if (sender.Balance < dto.Amount + fees)
    {
        return new ResponseDto<GetTransactionDto>
        {
            Success = false,
            Message = "Insufficient funds",
            Data = null
        };
    }

    var transaction = TransactionMapper.PostTransactionMapperToTransaction(dto);
    transaction.Fees = fees;

    var senderUpdateResult = await _accountService.UpdateAccountAsync(new UpdateAccountDto
    {
        AccountNumber = sender.AccountNumber,
        TopUp = (dto.Amount + fees) * -1
    });

    var receiverUpdateResult = await _accountService.UpdateAccountAsync(new UpdateAccountDto
    {
        AccountNumber = receiver.AccountNumber,
        TopUp = MoneyTransaction.ConvertCurrency(sender.Currency, receiver.Currency, dto.Amount)
    });

    var createTransactionResult = await _transactionRepo.CreateTransaction(transaction);

    if (!senderUpdateResult.Success || !receiverUpdateResult.Success || !createTransactionResult.Success)
    {
        return new ResponseDto<GetTransactionDto>
        {
            Success = false,
            Message = "Transaction failed",
            Data = null
        };
    }

    return createTransactionResult;
}

    public async Task<GetFullTransactionInfoDto?> GetFullTransactionInfo(int id)
    {
        return await _transactionRepo.GetFullTransactionInfo(id);
    }
}