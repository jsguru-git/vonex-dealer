using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class Message
    {

        public string To { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public string Attachment { get; set; }
        public string AttachmentName { get; set; }
        public Int64 OrderID { get; set; }

        public bool HasAttachment
        {
            get
            {
                if (Attachment != null && Attachment.Length > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
