using Microsoft.EntityFrameworkCore;
using api.core.Dtos.Shared;
using api.core.Dtos.Transaction;
using api.core.Interfaces.IRepository;
using api.core.Mapper;
using api.core.Models;
using api.Data;

namespace api.core.Repository;

public class TransactionRepo : ITransactionRepo
{
    private readonly ApplicationDbContext _context;

    public TransactionRepo(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<GetTransactionDto>?> GetAllTransactions()
    {
        var transactions = await ((IQueryable<Transaction>)_context.Transactions)
            .Select(t => new GetTransactionDto
            {
                TransactionId = t.TransactionId,
                TransactionDate = t.TransactionDate,
                Amount = t.Amount,
                Fees = t.Fees,
                SenderAccountId = t.SenderAccountId,
                ReceiverAccountId = t.ReceiverAccountId,
                Occasion = t.Occasion
            })
            .ToListAsync();

        return transactions;
    }

    public async Task<GetTransactionDto?> GetTransactionById(int id)
    {
        var transaction = await ((IQueryable<Transaction>)_context.Transactions)
            .Where(t => t.TransactionId == id)
            .Select(t => new GetTransactionDto
            {
                TransactionId = t.TransactionId,
                TransactionDate = t.TransactionDate,
                Amount = t.Amount,
                Fees = t.Fees,
                SenderAccountId = t.SenderAccountId,
                ReceiverAccountId = t.ReceiverAccountId,
                Occasion = t.Occasion
            })
            .FirstOrDefaultAsync();

        return transaction;
    }

    public async Task<ResponseDto<GetTransactionDto>> CreateTransaction(Transaction transaction)
    {
        try
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var createdTransaction = TransactionMapper.GetTransactionMapper(transaction);
            return new ResponseDto<GetTransactionDto>
            {
                Success = true,
                Message = "Transaction created successfully.",
                Data = createdTransaction
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<GetTransactionDto>
            {
                Success = false,
                Message = $"Error creating transaction: {ex.Message}",
                Data = null
            };
        }
    }

    public Task<bool> DeleteTransaction(int id)
    {
        var transaction = _context.Transactions.FirstOrDefault(t => t.TransactionId == id);
        if (transaction == null)
        {
            return Task.FromResult(false);
        }

        _context.Transactions.Remove(transaction);
        return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
    }

    public async Task<GetFullTransactionInfoDto?> GetFullTransactionInfo(int transactionId)
    {
        var transaction = await _context.Transactions
            .Include(t => t.SenderAccount)
            .Include(t => t.ReceiverAccount)
            .FirstOrDefaultAsync(t => t.TransactionId == transactionId);

        if (transaction == null || transaction.SenderAccount == null || transaction.ReceiverAccount == null)
        {
            return null;
        }

        return TransactionMapper.GetFullTransactionInfoMapper(transaction);
    }
}