using Microsoft.Exchange.WebServices.Data;
using System;
using Task = System.Threading.Tasks.Task;

namespace FacilityMonitoring.Common.Services {
    public class EmailService : IEmailService {
        private ExchangeService _exchange;

        public EmailService() {
            this._exchange= new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            WebCredentials credentials = new WebCredentials("facilityalerts", "Facility!1sskv", "sskep.com");
            this._exchange.Credentials = credentials;
            this._exchange.Url = new Uri(@"https://email.seoulsemicon.com/EWS/Exchange.asmx");
        }

        public void SendMessage(string msg) {
            EmailMessage message = new EmailMessage(this._exchange);
            message.ToRecipients.Add("aelmendorf@s-et.com");
            message.ToRecipients.Add("mestes@s-et.com");
            message.ToRecipients.Add("rakesh@s-et.com");
            message.ToRecipients.Add("rkennedy@s-et.com");
            message.ToRecipients.Add("bmurdaugh@s-et.com");
            message.Subject = "Facility Monitoring Alert";
            MessageBody body = new MessageBody();
            body.BodyType = BodyType.HTML;
            body.Text = msg;
            message.Body = body;
            message.SendAndSaveCopy();
        }

        public async Task SendMessageAsync(string msg) {
            EmailMessage message = new EmailMessage(this._exchange);
            message.ToRecipients.Add("aelmendorf@s-et.com");
            message.ToRecipients.Add("mestes@s-et.com");
            message.ToRecipients.Add("rakesh@s-et.com");
            message.ToRecipients.Add("rkennedy@s-et.com");
            message.ToRecipients.Add("bmurdaugh@s-et.com");
            message.Subject = "Facility Alert Service";
            MessageBody body = new MessageBody();
            body.BodyType = BodyType.HTML;
            body.Text = msg;
            message.Body = body;
            await message.SendAndSaveCopy();
        }


    }
}
