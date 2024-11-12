using FINAL_INTERN.Models.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Business.SendEmail
{
    public class SendEmailService
    {
        private readonly MailSetting _mailSetting;

        public SendEmailService(IOptions<MailSetting> mailSetting)
        {
            _mailSetting = mailSetting.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            // Đặt người gửi
            emailMessage.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Address_mail);
            emailMessage.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Address_mail));

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("The recipient's email address cannot be null or empty.", nameof(email));
            }

            // Đặt người nhận
            emailMessage.To.Add(new MailboxAddress("", email));

            // Đặt tiêu đề email
            emailMessage.Subject = subject;

            // Tạo nội dung email với định dạng HTML
            var builder = new BodyBuilder
            {
                HtmlBody = message
            };
            emailMessage.Body = builder.ToMessageBody();

            // Tạo đối tượng SmtpClient để gửi email
            using var smtpClient = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                // Kết nối tới server SMTP và gửi email
                await smtpClient.ConnectAsync(_mailSetting.Host, _mailSetting.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_mailSetting.Address_mail, _mailSetting.Password);
                await smtpClient.SendAsync(emailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
                throw;
            }
            finally
            {
                // Đóng kết nối SMTP
                await smtpClient.DisconnectAsync(true);
            }
        }
        public async Task<string> SendMain(MailContent mailContent)
        {
            await SendEmailAsync(mailContent.To, mailContent.Subject, mailContent.Body);
            return "Gửi thành công";
        }
    }
}
