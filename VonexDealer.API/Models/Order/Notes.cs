using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.Order
{
    public class Note
    {
        public Int64 NoteID { get; set; }
        public Int64 OrderID { get; set; }
        public string Component { get; set; }
        public string NoteText { get; set; }

    }
}
