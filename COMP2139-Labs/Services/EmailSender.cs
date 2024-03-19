using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SendWithBrevo;
using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Services {
    public class EmailSender : IEmailSender {
        private readonly string _brevoKey;

        public EmailSender(IConfiguration configuration) {
            _brevoKey = configuration["Brevo:ApiKey"];
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
            var client = new BrevoClient(_brevoKey);
            //var from = new Email

            throw new NotImplementedException();
        }
    }
}
