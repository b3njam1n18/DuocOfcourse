using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Auth_Credentials
    {
        [Key]
        public long UserId { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public bool EmailVerified { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? PasswordUpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Users User { get; set; }
    }
}
