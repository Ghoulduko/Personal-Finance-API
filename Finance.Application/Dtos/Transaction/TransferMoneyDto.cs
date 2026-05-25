namespace Finance.Application.Dtos.Transaction;

public class TransferMoneyDto
{
    public decimal Amount { get; set; }
    public string ReceiverEmail { get; set; }
}