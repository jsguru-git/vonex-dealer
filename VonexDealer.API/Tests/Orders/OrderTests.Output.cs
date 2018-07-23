using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using Xunit;

namespace VonexDealer.API.Tests.Orders
{
    public partial class OrderTests
    {

        [Fact(DisplayName ="SendEmail")]
        public async Task SendEmailAsync()
        {
            Message email = new Message{
                To="andrew.smith@alsconsulting.com.au",
                Subject="this is a test",
                EmailBody="<h3>This is a test message</h3>"
            };

            bool result = await _order.SendEmailAsync(email, null);
            Assert.True(result);
        }

        [Fact(DisplayName = "CreatePDF")]
        public void CreatePDF()
        {
            Assert.True(false);
        }

        [Fact(DisplayName ="EmailPDF")]
        public void EmailPDF()
        {
            Assert.True(false);
        }

        [Fact(DisplayName ="CreateUBCustomer")]
        public void CreateUBCustomer()
        {
            Assert.True(false);
        }
        [Fact(DisplayName ="CreateKayakoTicket")]
        public void CreateKayakoTicket()
        {
            Assert.True(false);
        }
    }
}
