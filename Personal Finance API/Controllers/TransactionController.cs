using Finance.Application.Dtos.Transaction;
using Finance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Personal_Finance_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("TransferMoney")]
    [Authorize]
    public async Task<Ok<string>> TransferMoney([FromBody] TransferMoneyDto req)
    {
        var userEmail = User.FindFirst("UserEmail")?.Value;
        await _transactionService.TransferMoney(userEmail, req);
        return TypedResults.Ok($"Transaction of ${req.Amount} was successful.");
    }

    [HttpGet("GetAllTransactionsUser")]
    [Authorize]
    public async Task<Ok<IEnumerable<TransactionDto>>> GetAllTransactionsUser()
    {
        var userId = User.FindFirst("Id")?.Value;
        return TypedResults.Ok(await _transactionService.GetUserTransactions(int.Parse(userId)));
    }

    [HttpGet("GetAllExistingTransactions")]
    [Authorize(Roles = "SUPERADMIN,OWNER")]
    public async Task<Ok<IEnumerable<TransactionDto>>> GetAllExistingTransactions()
    {
        return TypedResults.Ok(await _transactionService.GetExistingTransactions());
    }
}