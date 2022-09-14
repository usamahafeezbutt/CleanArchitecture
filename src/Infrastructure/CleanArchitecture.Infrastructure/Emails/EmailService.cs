using CleanArchitecture.Application.Common.Interfaces.Emails;
using CleanArchitecture.Application.Common.Models.Emails;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using CleanArchitecture.Application.Common.Configurations.Settings.Emails;

namespace CleanArchitecture.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public async Task Send(EmailOptions emailOptions)
        {
            MailMessage mail = InitializeMailMessage(emailOptions);
            PrepareRecepients(emailOptions, mail);
            var networkCredential = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);
            SmtpClient smtpClient = InitializeSmtpClient(networkCredential);
            await smtpClient.SendMailAsync(mail);
        }

        private SmtpClient InitializeSmtpClient(NetworkCredential networkCredential)
        => new()
            {
                Host = _smtpSettings.Host,
                Port = _smtpSettings.Port,
                EnableSsl = _smtpSettings.EnableSsl,
                UseDefaultCredentials = _smtpSettings.UseDefaultCredentials,
                Credentials = networkCredential
            };

        private MailMessage InitializeMailMessage(EmailOptions emailOptions)
        => new()
            {
                Subject = emailOptions.Subject,
                Body = emailOptions.Body,
                From = new MailAddress(_smtpSettings.Sender),
                IsBodyHtml = _smtpSettings.IsBodyHtml,
                BodyEncoding = Encoding.Default
            };

        private static void PrepareRecepients(EmailOptions emailOptions, MailMessage mail)
        {
            PrepareToRecepients(emailOptions, mail);
            PrepareCCRecepients(emailOptions.CCEmails!, mail);
            PrepareBccRecepients(emailOptions.BccEmails!, mail);
        }

        private static void PrepareToRecepients(EmailOptions emailOptions, MailMessage mail)
        {
            foreach (var toEmail in emailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }
        }

        private static void PrepareBccRecepients(List<string> bccEmails, MailMessage mail)
        {
            if (bccEmails is not null && bccEmails.Any())
            {
                foreach (var bccEmail in bccEmails!)
                {
                    mail.Bcc.Add(bccEmail);
                }
            }
        }

        private static void PrepareCCRecepients(List<string> ccEmails, MailMessage mail)
        {
            if (ccEmails is not null && ccEmails.Any())
            {
                foreach (var ccEmail in ccEmails)
                {
                    mail.CC.Add(ccEmail);
                }
            }
        }
    }
}
