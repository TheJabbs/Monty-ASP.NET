using api.core.Dtos.Transaction;
using api.core.Service;
using Microsoft.AspNetCore.Mvc;

namespace api.core.Controller;

[Route("api/Transaction")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;
    
    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        var transactions = await _transactionService.GetAllTransactions();
        if (transactions == null || !transactions.Any())
        {
            return NotFound("No transactions found.");
        }
        
        return Ok(transactions);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        var transaction = await _transactionService.GetTransactionById(id);
        if (transaction == null)
        {
            return NotFound($"Transaction with ID {id} not found.");
        }
        
        return Ok(transaction);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] PostTransactionDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _transactionService.CreateTransaction(dto);
        if (response == null || !response.Success)
        {
            return BadRequest(response?.Message ?? "Failed to create transaction.");
        }
        
        return CreatedAtAction(nameof(GetTransactionById), new { id = response.Data.TransactionId }, response.Data);
    }
    
    [HttpGet("full/{id}")]
    public async Task<IActionResult> GetFullTransactionById(int id)
    {
        var transaction = await _transactionService.GetFullTransactionInfo(id);
        if (transaction == null)
        {
            return NotFound($"Transaction with ID {id} not found.");
        }
        
        return Ok(transaction);
    }
    
    
}