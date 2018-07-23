using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VonexDealer.API.Models
{
    public class DataDisplayManager
    {
        public string SortBy { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
        public bool Descending { get; set; }
        public Int64 TotalItems { get; set; }

    }
}
