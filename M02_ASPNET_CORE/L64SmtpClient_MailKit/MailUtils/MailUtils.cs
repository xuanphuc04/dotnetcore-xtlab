using System.Net;
using System.Net.Mail;

namespace L64SmtpClient_MailKit.MailUtils
{

    public class MailUtils
    {
        // Sử dụng localhost
        public static async Task<string> SendMail(
            string _from,
            string _to,
            string _subject,
            string _body
        )
        {
            // Message sẽ được SmtpClient kết nối với server
            // để gửi đi
            MailMessage message = new MailMessage(_from, _to, _subject, _body);
            // Thiết lập thêm các thuộc tính
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Nội dung có thể là HTML
            message.IsBodyHtml = true;

            // Thiết lập địa chỉ mail nhận
            // nếu người dùng Reply
            message.ReplyToList.Add(new MailAddress(_from));

            // Thiết lập thông tin về người gửi
            message.Sender = new MailAddress(_from);

            // Tạo ra một SmtpClient để gửi message
            using var smtpClient = new SmtpClient("localhost");

            try
            {
                // smtpClient.Send(message); // Với phương thức đồng bộ
                await smtpClient.SendMailAsync(message);
                return "Send mail success";
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return "Send mail failed";
            }
        }

        // Sử dụng Gmail 
        public static async Task<string> SendGMail(
            string _from,
            string _to,
            string _subject,
            string _body,
            string _gmail,
            string _password
        )
        {
            // Message sẽ được SmtpClient kết nối với server
            // để gửi đi
            MailMessage message = new MailMessage(_from, _to, _subject, _body);
            // Thiết lập thêm các thuộc tính
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Nội dung có thể là HTML
            message.IsBodyHtml = true;

            // Thiết lập địa chỉ mail nhận
            // nếu người dùng Reply
            message.ReplyToList.Add(new MailAddress(_from));

            // Thiết lập thông tin về người gửi
            message.Sender = new MailAddress(_from);

            // Tạo ra một SmtpClient để gửi message
            using var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_gmail, _password);

            try
            {
                // smtpClient.Send(message); // Với phương thức đồng bộ
                await smtpClient.SendMailAsync(message);
                return "Send mail success";
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return "Send mail failed";
            }
        }
    }
}