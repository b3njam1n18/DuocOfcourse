using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Users
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? SecondLastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Roles Role { get; set; }

        public ICollection<Password_Reset_Token> PasswordResetTokens { get; set; } = new List<Password_Reset_Token>();

        public ICollection<Auth_Credentials> AuthCredentials { get; set; }
        public ICollection<Notifications> Notifications { get; set; }
        public ICollection<Attempts> Attempts { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; }
        public ICollection<Courses> CoursesTaught { get; set; }
        public ICollection<Media> UploadedMedia { get; set; }
    }
}
