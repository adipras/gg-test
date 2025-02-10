using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace gg_test.Models
{
    public class EmailHistory
    {
        public int Id { get; set; }
        public string Recipient { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        [Column(TypeName = "timestamp")]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
