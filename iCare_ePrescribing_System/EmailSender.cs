using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;


namespace iCare_ePrescribing_System
{
    public class EmailSender : IEmailSender
    {

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "gxekiwe48@gmail.com";
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Bay Breeze", mail)); 
            mimeMessage.To.Add(new MailboxAddress(null, email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain") { Text = message };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("gxekiwe48@gmail.com" ,"ynfo sodu thlq bjol");
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}