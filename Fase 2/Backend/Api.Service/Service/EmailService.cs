using System.Net.Mail;
using Api.Core.IServices;
using MailKit.Net.Smtp;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace Api.Service.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Soporte DuocOfCourse", "no-reply@duocofcourse.cl"));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("benjamin.gamboa.gb@gmail.com", "rbyj fuzj qysz fhau");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
