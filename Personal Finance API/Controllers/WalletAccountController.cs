using Finance.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Personal_Finance_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletAccountController : Controller
{
    private readonly IWalletAccountService _service;

    public WalletAccountController(IWalletAccountService service)
    {
        _service = service;
    }

    [HttpPost("Deposit/{amount:decimal}")]
    public async Task<Ok<string>> Deposit(decimal amount)
    {
        var userId = User.FindFirst("Id")?.Value;
        await _service.Deposit(amount, int.Parse(userId));
        return TypedResults.Ok($"Successfully deposited ${amount} to your account");
    }
    
    [HttpPost("Withdraw/{amount:decimal}")]
    public async Task<Ok<string>> Withdraw(decimal amount)
    {
        var userId = User.FindFirst("Id")?.Value;
        await _service.Withdraw(amount, int.Parse(userId));
        return TypedResults.Ok($"Successfully Withdrew ${amount} from your account.");
    }

    [HttpGet("CheckBalance")]
    public async Task<Ok<string>> CheckBalance()
    {
        var userId = User.FindFirst("Id")?.Value;
        decimal userBalance = await _service.CheckBalance(int.Parse(userId));
        return TypedResults.Ok($"Your account balance is ${userBalance}");
    }
}