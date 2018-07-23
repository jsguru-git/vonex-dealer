using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using VonexDealer.API.Models.Order;
using System.Security.Claims;
using VonexDealer.API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {
        /// <summary>
        /// create email with order details - send to dealer email. 
        /// And also send to helpdesk.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
		public async Task<bool> SendEmailAsync(Message email, ClaimsPrincipal dealer)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress( _config["Smtp:FromEmail"]));

            _logger.LogDebug("mail from Vue send: {Message}", email);

            string dealerEmail = loggedInDealerEmail(dealer);
            if(dealerEmail == null)
            {
                dealerEmail = email.To ?? "andrew.smith@alsconsulting.com.au";
            }

            if (Debugger.IsAttached || _env.IsDevelopment())
            {
                message.To.Clear();
                message.To.Add(new MailboxAddress("Andrew", "andrew.smith@alsconsulting.com.au"));
            }
            else
            {
                message.To.Add(new MailboxAddress(dealerEmail));
                _logger.LogDebug($"SendGeneralEmail: {message.To}");

            }

            var builder = new BodyBuilder();

            builder.HtmlBody = email.EmailBody;

            // create report as attachment
            if (email.HasAttachment)
            {
                //var report = new ActionAsPdf("OrderReport", new { orderID = 7 });
                //byte[] byteArray = report.BuildFile(null);
                //MemoryStream stream = new MemoryStream(byteArray);
                //builder.Attachments.Add($"{email.AttachmentName}.pdf", byteArray);
            }
            // Now we just need to set the message body and we're done
            message.Body = builder.ToMessageBody();
            message.Subject = email.Subject;

            _logger.LogDebug("mail to send: {Message}", email);

            _logger.LogDebug("mail compiled to send: {MimeMessage}", message);
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(_config["Smtp:EmailServer"]
                        , Convert.ToInt32(_config["Smtp:SMTPPort"]), SecureSocketOptions.Auto);
                    client.Authenticate(_config["Smtp:EmailID"], _config["Smtp:EmailPwd"]);
                    client.MessageSent += Client_MessageSent;
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (SmtpCommandException ex)
            {
                _logger.LogError($"Email Error message not sent: {ex.Message}");
                return false;
            }

            return true;
        }

        private void Client_MessageSent(object sender, MessageSentEventArgs e)
        {
            _logger.LogInformation($"Message sent {e.Message.To}");
        }


        public async Task<OrderView> GetOrderReport(Int64 orderID, Guid orderGUID = new Guid())
        {

            if (orderID == 0)
            {
                //get orderID from guid.
                Order result = await _context.Orders.Where(x => x.OrderGUID == orderGUID).FirstOrDefaultAsync();
                if (result == null)
                    return null;
                orderID = result.OrderID;
            }

            OrderView orderView = new OrderView();
            orderView.Addresses = await GetAddressesOnOrderAsync(orderID);
            orderView.ratePlans = GetRatePlans().ToList();

            
            Order order = GetOrders(orderID, "").FirstOrDefault();
            if (order == null)
                return null;

            orderView.order = order;

            Int64 dealerID = order.DealerID; //_order.loggedInDealerID(User);
            Dealer dealer = await getDealerAsync(dealerID);
            orderView.dealer = dealer;

            if (order.CustomerDetails != null && order.CustomerDetails.CompanyID != null && order.CustomerDetails.CompanyID > 0)
            {
                orderView.company = await GetCompanyAsync(order.CustomerDetails.CompanyID ?? 0);
            }


            if (order.LandlineID > 0)
            {
                List<Churn> churns = await _landline.GetChurnsOnOrder(orderID);
                if (churns.Count > 0)
                    orderView.churns = churns;

                List<ISDN> isdns = await _landline.GetISDNsOnOrderAsync(orderID);
                if (isdns.Count > 0)
                    orderView.isdn = isdns;

                List<NewPSTN> newServices = await _landline.GetNewPSTNsOnOrder(orderID);
                if (newServices != null)
                    orderView.newServices = newServices;

            }

            //IPOrder
            if (order.IPOrderID > 0)
            {
                List<HandsetView> handsets = await getHandsetsAsync();
                if (handsets.Count > 0)
                    orderView.handsets = handsets;

                List<IPExtension> iPExtensions = await GetIPExtensionsOnOrderAsync(orderID);
                if (iPExtensions != null)
                    orderView.iPExtensions = iPExtensions;

                IPOrder iPOrder = await GetIpOnOrderAsync(orderID);
                if (iPOrder != null)
                    orderView.iPOrder = iPOrder;

            }

            List<Internet> internets = await getInternetOnOrderAsync(orderID);
            if (internets != null)
                orderView.Internets = internets;

            List<Contact> contacts = await GetContactsAsync(orderID);
            if (contacts.Count > 0)
                orderView.Contacts = contacts;


            if (order.HardwareID > 0)
            {
                HardwareOrder hardwareOrder = await GetHardwareOrderAsync(orderID);
                List<Hardware> hardware = await GetHardwareOnOrderAsync(orderID);
                orderView.hardware = hardware;
            }
            orderView.Notes = await GetNotesOnOrderAsync(orderID);

            return orderView;

        }

    }
}
