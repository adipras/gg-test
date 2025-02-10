using gg_test.Services;
using Microsoft.AspNetCore.Mvc;
using gg_test.Data;
using gg_test.Models;
using Microsoft.EntityFrameworkCore;

namespace gg_test.Controllers
{
    [ApiController]
    [Route("api/emails")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly ApplicationDbContext _dbContext;

        public EmailController(EmailService emailService, ApplicationDbContext dbContext)
        {
            _emailService = emailService;
            _dbContext = dbContext;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Recipient) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
                return BadRequest("All fields are required.");

            var result = await _emailService.SendEmailAsync(request.Recipient, request.Subject, request.Body);
            if (result)
                return Ok(new { message = "Email sent successfully." });

            return StatusCode(500, "Failed to send email.");
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetEmailHistory()
        {
            var emails = await _dbContext.EmailHistories.OrderByDescending(e => e.SentAt).ToListAsync();
            return Ok(emails);
        }
    }

    public class EmailRequest
    {
        public string Recipient { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
