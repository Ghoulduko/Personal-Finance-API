using System.Net;
using System.Net.Mail;
using Finance.Application.Interfaces;

namespace Finance.Application.Services.Notifications;

public class EmailNotificationService : IEmailNotificationService
{
    public async Task SendRegisterEmail(string email, string username)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("gelab2109@gmail.com", "wulyylslqxdqgtvi"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("gelab2109@gmail.com"),
            Subject = "Welcome to Order Management 🎉",
            Body = $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"" />
                    <title>Welcome</title>
                </head>

                <body style=""margin:0; padding:0; background:#f5f6fa; font-family:Arial, Helvetica, sans-serif;"">

                <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f6fa; padding:24px 0;"">
                <tr>
                <td align=""center"">

                <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);"">

                <tr>
                <td style=""padding:22px 28px; background:#1d4ed8;"">

                <div style=""color:#ffffff; font-size:18px; font-weight:700;"">
                Account Created Successfully 🎉
                </div>

                <div style=""color:#dbeafe; font-size:13px; margin-top:6px;"">
                Welcome to Order Management
                </div>

                </td>
                </tr>

                <tr>
                <td style=""padding:28px;"">

                <div style=""font-size:15px; color:#111827; line-height:1.6;"">
                Hello, {username}!<br /><br />
                Your account has been created successfully and your wallet is now ready to use.
                </div>

                <div style=""margin:18px 0 10px; text-align:center;"">

                <div style=""display:inline-block; padding:14px 18px; border-radius:12px; background:#eff6ff; border:1px solid #bfdbfe;"">

                <span style=""font-size:16px; font-weight:700; color:#1d4ed8;"">
                Username: {username}<br/>
                Created At: {DateTime.UtcNow}
                </span>

                </div>
                </div>

                <div style=""font-size:13px; color:#6b7280; margin-top:12px;"">
                You can now securely log in and start using the platform.
                </div>

                <hr style=""border:none; border-top:1px solid #e5e7eb; margin:22px 0;"" />

                <div style=""font-size:12px; color:#9ca3af;"">
                Thank you for choosing Order Management.
                </div>

                </td>
                </tr>

                <tr>
                <td style=""padding:18px 28px; background:#f9fafb; font-size:12px; color:#9ca3af;"">
                © Order Management • Automated message
                </td>
                </tr>

                </table>
                </td>
                </tr>
                </table>

                </body>
                </html>",

            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtp.SendMailAsync(mailMessage);
    }


    public async Task SendLoginEmail(string email, string username)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("gelab2109@gmail.com", "wulyylslqxdqgtvi"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("gelab2109@gmail.com"),
            Subject = "New Login Detected",
            Body = $@"<!DOCTYPE html>
            <html lang=""en"">
            <head>
              <meta charset=""UTF-8"" />
              <title>Login Notification</title>
            </head>
            <body style=""margin:0; padding:0; background:#f5f6fa; font-family:Arial, Helvetica, sans-serif;"">
              <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f6fa; padding:24px 0;"">
                <tr>
                  <td align=""center"">
                    <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);"">
                      
                      <tr>
                        <td style=""padding:22px 28px; background:#111827;"">
                          <div style=""color:#ffffff; font-size:18px; font-weight:700;"">
                            Login Successful 🔐
                          </div>
                          <div style=""color:#cbd5e1; font-size:13px; margin-top:6px;"">
                            Your account was accessed successfully
                          </div>
                        </td>
                      </tr>

                      <tr>
                        <td style=""padding:28px;"">
                          <div style=""font-size:15px; color:#111827; line-height:1.6;"">
                            Hello, {username}!<br /><br />
                            We detected a successful login to your account.
                          </div>

                          <div style=""margin:18px 0 10px; text-align:center;"">
                            <div style=""display:inline-block; padding:14px 18px; border-radius:12px; background:#f3f4f6; border:1px solid #e5e7eb;"">
                              <span style=""font-size:16px; font-weight:700; color:#111827;"">
                                Login Time: {DateTime.UtcNow}<br/>
                                Account: {username}
                              </span>
                            </div>
                          </div>

                          <div style=""font-size:13px; color:#6b7280; margin-top:12px;"">
                            If this was you, no further action is needed.
                          </div>

                          <hr style=""border:none; border-top:1px solid #e5e7eb; margin:22px 0;"" />

                          <div style=""font-size:12px; color:#9ca3af;"">
                            If you did not log into your account, please change your password immediately.
                          </div>
                        </td>
                      </tr>

                      <tr>
                        <td style=""padding:18px 28px; background:#f9fafb; font-size:12px; color:#9ca3af;"">
                          © Order Management • This is an automated message, please don't reply.
                        </td>
                      </tr>

                    </table>
                  </td>
                </tr>
              </table>
            </body>
            </html>",
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtp.SendMailAsync(mailMessage);
    }

    public async Task SendDepositEmail(string email, string username, decimal amount)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("gelab2109@gmail.com", "wulyylslqxdqgtvi"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("gelab2109@gmail.com"),
            Subject = "Deposit Successful 💰",
            Body = $@"<!DOCTYPE html>
                <html lang=""en"">
                <head>
                <meta charset=""UTF-8"" />
                <title>Deposit Notification</title>
                </head>

                <body style=""margin:0; padding:0; background:#f5f6fa; font-family:Arial, Helvetica, sans-serif;"">
                <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f6fa; padding:24px 0;"">
                <tr>
                <td align=""center"">

                <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);"">

                <tr>
                 <td style=""padding:22px 28px; background:#065f46;"">
                <div style=""color:#ffffff; font-size:18px; font-weight:700;"">
                Deposit Successful 💸
                </div>

                <div style=""color:#d1fae5; font-size:13px; margin-top:6px;"">
                Funds were added to your wallet successfully
                </div>
                </td>
                </tr>

                <tr>
                <td style=""padding:28px;"">

                <div style=""font-size:15px; color:#111827; line-height:1.6;"">
                Hello, {username}!<br /><br />
                A successful deposit was made to your wallet.
                </div>

                <div style=""margin:18px 0 10px; text-align:center;"">
                <div style=""display:inline-block; padding:14px 18px; border-radius:12px; background:#ecfdf5; border:1px solid #a7f3d0;"">

                <span style=""font-size:16px; font-weight:700; color:#065f46;"">
                Amount Deposited: ${amount}<br/>
                </span>

                </div>
                </div>

                <div style=""font-size:13px; color:#6b7280; margin-top:12px;"">
                Your wallet balance has been updated successfully.
                </div>

                <hr style=""border:none; border-top:1px solid #e5e7eb; margin:22px 0;"" />

                <div style=""font-size:12px; color:#9ca3af;"">
                If you did not make this deposit, please secure your account immediately.
                </div>

                </td>
                </tr>

                <tr>
                <td style=""padding:18px 28px; background:#f9fafb; font-size:12px; color:#9ca3af;"">
                © Order Management • Automated message
                </td>
                </tr>

                </table>
                </td>
                </tr>
                </table>
                </body>
                </html>",
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtp.SendMailAsync(mailMessage);
    }


    public async Task SendWithdrawEmail(string email, string username, decimal amount)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("gelab2109@gmail.com", "wulyylslqxdqgtvi"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("gelab2109@gmail.com"),
            Subject = "Withdrawal Successful 💳",
            Body = $@"<!DOCTYPE html>
              <html lang=""en"">
              <head>
              <meta charset=""UTF-8"" />
              <title>Withdrawal Notification</title>
              </head>

              <body style=""margin:0; padding:0; background:#f5f6fa; font-family:Arial, Helvetica, sans-serif;"">
              <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f6fa; padding:24px 0;"">
              <tr>
              <td align=""center"">

              <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);"">

              <tr>
              <td style=""padding:22px 28px; background:#7f1d1d;"">
              <div style=""color:#ffffff; font-size:18px; font-weight:700;"">
              Withdrawal Successful 💳
              </div>

              <div style=""color:#fecaca; font-size:13px; margin-top:6px;"">
              Funds were withdrawn from your wallet
              </div>
              </td>
              </tr>

              <tr>
              <td style=""padding:28px;"">

              <div style=""font-size:15px; color:#111827; line-height:1.6;"">
              Hello, {username}!<br /><br />
              A withdrawal was processed successfully from your wallet.
              </div>

              <div style=""margin:18px 0 10px; text-align:center;"">
              <div style=""display:inline-block; padding:14px 18px; border-radius:12px; background:#fef2f2; border:1px solid #fecaca;"">

              <span style=""font-size:16px; font-weight:700; color:#7f1d1d;"">
              Amount Withdrawn: ${amount}<br/>
              </span>

              </div>
              </div>

              <div style=""font-size:13px; color:#6b7280; margin-top:12px;"">
              Your wallet balance has been updated.
              </div>

              <hr style=""border:none; border-top:1px solid #e5e7eb; margin:22px 0;"" />

              <div style=""font-size:12px; color:#9ca3af;"">
              If you did not make this withdrawal, please secure your account immediately.
              </div>

              </td>
              </tr>

              <tr>
              <td style=""padding:18px 28px; background:#f9fafb; font-size:12px; color:#9ca3af;"">
              © Order Management • Automated message
              </td>
              </tr>

              </table>
              </td>
              </tr>
              </table>
              </body>
              </html>",
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtp.SendMailAsync(mailMessage);
    }

    public async Task SendReceiveTransferredEmail(string email, string username, string senderUsername, decimal amount)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("gelab2109@gmail.com", "wulyylslqxdqgtvi"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("gelab2109@gmail.com"),
            Subject = "Money Received 💰",

            Body = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"" />
                <title>Money Received</title>
            </head>

            <body style=""margin:0; padding:0; background:#f5f6fa; font-family:Arial, Helvetica, sans-serif;"">

            <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f6fa; padding:24px 0;"">
            <tr>
            <td align=""center"">

            <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);"">

            <tr>
            <td style=""padding:22px 28px; background:#047857;"">

            <div style=""color:#ffffff; font-size:18px; font-weight:700;"">
            Money Received 💰
            </div>

            <div style=""color:#d1fae5; font-size:13px; margin-top:6px;"">
            Funds were added to your wallet
            </div>

            </td>
            </tr>

            <tr>
            <td style=""padding:28px;"">

            <div style=""font-size:15px; color:#111827; line-height:1.6;"">
            Hello, {username}!<br /><br />
            You have successfully received money into your wallet.
            </div>

            <div style=""margin:18px 0 10px; text-align:center;"">

            <div style=""display:inline-block; padding:14px 18px; border-radius:12px; background:#ecfdf5; border:1px solid #a7f3d0;"">

            <span style=""font-size:16px; font-weight:700; color:#047857;"">
            Received Amount: ${amount}<br/>
            Sender: {senderUsername}<br/>
            </span>

            </div>
            </div>

            <div style=""font-size:13px; color:#6b7280; margin-top:12px;"">
            The funds are now available in your wallet.
            </div>

            <hr style=""border:none; border-top:1px solid #e5e7eb; margin:22px 0;"" />

            <div style=""font-size:12px; color:#9ca3af;"">
            Thank you for using Order Management.
            </div>

            </td>
            </tr>

            <tr>
            <td style=""padding:18px 28px; background:#f9fafb; font-size:12px; color:#9ca3af;"">
            © Order Management • Automated message
            </td>
            </tr>

            </table>
            </td>
            </tr>
            </table>

            </body>
            </html>",
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtp.SendMailAsync(mailMessage);
    }

    public async Task SendMoneyTransferredEmail(string email, string username, string receiverUsername, decimal amount)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("gelab2109@gmail.com", "wulyylslqxdqgtvi"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("gelab2109@gmail.com"),
            Subject = "Money Sent 💸",

            Body = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"" />
                <title>Money Sent</title>
            </head>

            <body style=""margin:0; padding:0; background:#f5f6fa; font-family:Arial, Helvetica, sans-serif;"">

            <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f6fa; padding:24px 0;"">
            <tr>
            <td align=""center"">

            <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);"">

            <tr>
            <td style=""padding:22px 28px; background:#7c2d12;"">

            <div style=""color:#ffffff; font-size:18px; font-weight:700;"">
            Money Sent 💸
            </div>

            <div style=""color:#fed7aa; font-size:13px; margin-top:6px;"">
            Funds were transferred successfully
            </div>

            </td>
            </tr>

            <tr>
            <td style=""padding:28px;"">

            <div style=""font-size:15px; color:#111827; line-height:1.6;"">
            Hello, {username}!<br /><br />
            Your transfer has been completed successfully.
            </div>

            <div style=""margin:18px 0 10px; text-align:center;"">

            <div style=""display:inline-block; padding:14px 18px; border-radius:12px; background:#fff7ed; border:1px solid #fdba74;"">

            <span style=""font-size:16px; font-weight:700; color:#7c2d12;"">
            Sent Amount: ${amount}<br/>
            Receiver: {receiverUsername}<br/>
            </span>

            </div>
            </div>

            <div style=""font-size:13px; color:#6b7280; margin-top:12px;"">
            The receiver should receive the funds shortly.
            </div>

            <hr style=""border:none; border-top:1px solid #e5e7eb; margin:22px 0;"" />

            <div style=""font-size:12px; color:#9ca3af;"">
            If you did not make this transfer, secure your account immediately.
            </div>

            </td>
            </tr>

            <tr>
            <td style=""padding:18px 28px; background:#f9fafb; font-size:12px; color:#9ca3af;"">
            © Order Management • Automated message
            </td>
            </tr>

            </table>
            </td>
            </tr>
            </table>

            </body>
            </html>",
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtp.SendMailAsync(mailMessage);
    }

    public async Task SendAccountDeletedEmail(string email, string username)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("gelab2109@gmail.com", "wulyylslqxdqgtvi"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("[gelab2109@gmail.com](mailto:gelab2109@gmail.com)"),
            Subject = "Account Deleted",
            Body = $@"<!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"" />
                <title>Account Deleted</title>
            </head>
            <body style=""margin:0; padding:0; background:#f5f6fa; font-family:Arial, Helvetica, sans-serif;"">
                <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f6fa; padding:24px 0;"">
                    <tr>
                        <td align=""center"">
                            <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);"">

            ```
                            <tr>
                                <td style=""padding:22px 28px; background:#111827;"">
                                    <div style=""color:#ffffff; font-size:18px; font-weight:700;"">
                                        Account Deleted 🗑️
                                    </div>
                                    <div style=""color:#cbd5e1; font-size:13px; margin-top:6px;"">
                                        Your account has been permanently removed
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td style=""padding:28px;"">
                                    <div style=""font-size:15px; color:#111827; line-height:1.6;"">
                                        Hello, {username}!<br /><br />
                                        This email confirms that your account has been successfully deleted.
                                    </div>

                                    <div style=""margin:18px 0 10px; text-align:center;"">
                                        <div style=""display:inline-block; padding:14px 18px; border-radius:12px; background:#f3f4f6; border:1px solid #e5e7eb;"">
                                            <span style=""font-size:16px; font-weight:700; color:#111827;"">
                                                Deletion Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC<br/>
                                                Account: {username}
                                            </span>
                                        </div>
                                    </div>

                                    <div style=""font-size:13px; color:#6b7280; margin-top:12px;"">
                                        All associated account data has been removed according to our deletion policy.
                                    </div>

                                    <hr style=""border:none; border-top:1px solid #e5e7eb; margin:22px 0;"" />

                                    <div style=""font-size:12px; color:#9ca3af;"">
                                        If you did not request this deletion, please contact support immediately.
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td style=""padding:18px 28px; background:#f9fafb; font-size:12px; color:#9ca3af;"">
                                    © Order Management • This is an automated message, please don't reply.
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
            ```

            </body>
            </html>",
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtp.SendMailAsync(mailMessage);
    }
}