using gg_test.Data;
using gg_test.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace gg_test.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public EmailService(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<bool> SendEmailAsync(string recipient, string subject, string body)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("No Reply", smtpSettings["FromEmail"]));
                message.To.Add(new MailboxAddress("", recipient));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = body };

                using var client = new SmtpClient();
                await client.ConnectAsync(smtpSettings["Host"], int.Parse(smtpSettings["Port"]!), false);
                await client.AuthenticateAsync(smtpSettings["Username"], smtpSettings["Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                // Save email history
                var emailHistory = new EmailHistory
                {
                    Recipient = recipient,
                    Subject = subject,
                    Body = body,
                    SentAt = DateTime.UtcNow
                };

                _dbContext.EmailHistories.Add(emailHistory);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
