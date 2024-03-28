using FluentEmail.Mailgun;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Services {
    public class EmailSender : IEmailSender {
        private readonly string _mailGunKey;

        public EmailSender(IConfiguration configuration) {
            _mailGunKey = configuration["MailGun:ApiKey"];
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
            var client = new MailgunSender("justin.oskam@georgebrown.ca", _mailGunKey);

            throw new NotImplementedException();
        }
    }
}