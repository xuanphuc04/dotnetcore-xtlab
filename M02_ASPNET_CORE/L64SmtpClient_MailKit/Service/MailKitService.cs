using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;

namespace L64SmtpClient_MailKit.Service
{
    public class MailKitService
    {
        MailSettings _mailSettings { set; get; }
        public MailKitService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<string> SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            // Thiết lập người gửi
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));

            // Thiết lập người nhận
            email.To.Add(new MailboxAddress(mailContent.To, mailContent.To));
            email.Subject = mailContent.Subject;

            // Thiết lập body (text, html, file đính kèm,...)
            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            // builder.TextBody
            // builder.Attachments
            //...
            email.Body = builder.ToMessageBody();


            // Tạo đối tượng Smtp để gửi email đi
            // sử dụng smtp của Mailkit chứ không dùng của System
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                await smtp.DisconnectAsync(true);
                return "Email sending failed";
            }

            await smtp.DisconnectAsync(true);
            return "Email sent successfully";
        }
    }

    public class MailContent
    {
        public string? To { set; get; }
        public string? Subject { set; get; }
        public string? Body { set; get; }
    }
}