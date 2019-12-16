using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Data.Entities {
    public class EmailRecipients {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public EmailRecipients(string name,string email) {
            this.Name = name;
            this.Email = email;
        }

        public EmailRecipients() {
            this.Name = "";
            this.Email = "";
        }
    }
}
