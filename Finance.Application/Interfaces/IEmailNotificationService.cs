namespace Finance.Application.Interfaces;

public interface IEmailNotificationService
{
    Task SendRegisterEmail(string email, string username);
    Task SendLoginEmail(string email, string username);
    Task SendDepositEmail(string email, string username, decimal amount);
    Task SendWithdrawEmail(string email, string username, decimal amount);
    Task SendReceiveTransferredEmail(string email, string username, string senderUsername, decimal amount);
    Task SendMoneyTransferredEmail(string email, string username, string receiverUsername, decimal amount);
}