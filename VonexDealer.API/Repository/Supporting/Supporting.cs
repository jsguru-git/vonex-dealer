using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Data;

namespace VonexDealer.API.Repository.Supporting
{
    public partial class Supporting : ISupport
    {
        private ApplicationDbContext _context;
        private ILogger<Supporting> _logger;

        public Supporting(ApplicationDbContext context, ILogger<Supporting> logger)
        {
            _context = context;
            _logger = logger;
        }
        
    }
}
