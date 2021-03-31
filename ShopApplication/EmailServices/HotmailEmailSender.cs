using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ShopApplication.EmailServices
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host;
        private int _port;
        private bool _enabledSsl;
        private string _userName;
        private string _password;
        public SmtpEmailSender(string host, int port, bool enabledSsl, string userName, string password)
        {
            _host = host;
            _port = port;
            _enabledSsl = enabledSsl;
            _userName = userName;
            _password = password;

        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enabledSsl
            };
            return client.SendMailAsync(
                new MailMessage(_userName, email, subject, htmlMessage)
                {
                    IsBodyHtml = true
                }
            );
        }
    }
}
