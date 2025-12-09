using System.Collections.Generic;
using Api.Core.Models;

public class Users
{
    public long Id { get; set; }
    public long RoleId { get; set; }

    public long? CareerId { get; set; }

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

    // 👇 Usa List<> en vez de new()
    public ICollection<Password_Reset_Token> PasswordResetTokens { get; set; }
        = new List<Password_Reset_Token>();

    public ICollection<Auth_Credentials> AuthCredentials { get; set; }
        = new List<Auth_Credentials>();

    public ICollection<Notifications> Notifications { get; set; }
        = new List<Notifications>();

    public ICollection<Attempts> Attempts { get; set; }
        = new List<Attempts>();

    public ICollection<Enrollments> Enrollments { get; set; }
        = new List<Enrollments>();

    public ICollection<Courses> CoursesTaught { get; set; }
        = new List<Courses>();

    public ICollection<Media> UploadedMedia { get; set; }
        = new List<Media>();

    // Navegaciones nuevas
    public Course_Categories? Career { get; set; }
}
