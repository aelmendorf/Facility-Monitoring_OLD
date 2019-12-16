using Microsoft.Exchange.WebServices.Data;
using System;
using Task = System.Threading.Tasks.Task;
using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FacilityMonitoring.Common.Services {
    public class EmailService : IEmailService {
        private ExchangeService _exchange;
        private readonly FacilityContext _context;
        private List<EmailRecipients> _recipients;

        public EmailService(FacilityContext context) {
            this._context = context;
            this._exchange= new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            WebCredentials credentials = new WebCredentials("facilityalerts", "Facility!1sskv", "sskep.com");
            this._exchange.Credentials = credentials;
            this._exchange.Url = new Uri(@"https://email.seoulsemicon.com/EWS/Exchange.asmx");
            this.GetRecipients();
        }

        private void GetRecipients() {
            this._recipients = this._context.GetEmailRecipients();
        }

        public void SendMessage(string msg) {
            EmailMessage message = new EmailMessage(this._exchange);
            message.ToRecipients.AddRange(this._recipients.Select(e=>e.Email));
            //message.ToRecipients.Add("aelmendorf@s-et.com");
            //message.ToRecipients.Add("mestes@s-et.com");
            //message.ToRecipients.Add("rakesh@s-et.com");
            //message.ToRecipients.Add("rkennedy@s-et.com");
            //message.ToRecipients.Add("bmurdaugh@s-et.com");
            message.Subject = "Facility Monitoring Alert";
            MessageBody body = new MessageBody();
            body.BodyType = BodyType.HTML;
            body.Text = msg;
            message.Body = body;
            message.SendAndSaveCopy();
        }

        public async Task SendMessageAsync(string msg) {
            EmailMessage message = new EmailMessage(this._exchange);
            message.ToRecipients.AddRange(this._recipients.Select(e => e.Email));
            //message.ToRecipients.Add("aelmendorf@s-et.com");
            //message.ToRecipients.Add("mestes@s-et.com");
            //message.ToRecipients.Add("rakesh@s-et.com");
            //message.ToRecipients.Add("rkennedy@s-et.com");
            //message.ToRecipients.Add("bmurdaugh@s-et.com");
            message.Subject = "Facility Alert Service";
            MessageBody body = new MessageBody();
            body.BodyType = BodyType.HTML;
            body.Text = msg;
            message.Body = body;
            await message.SendAndSaveCopy();
        }


    }
}
