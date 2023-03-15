using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorSystem.Contexts;
using ErrorSystem.Models.Entities;
using ErrorSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ErrorSystem.Services
{
    internal class StatusReportService
    {
        private static readonly DataContext _context = new();
        public static async Task CreateAsync(StatusReport statusReport)
        {

            var _statusReportEntity = new StatusReportEntity
            {
                StatusName = statusReport.StatusName, 
                DateUpdated = DateTime.Now,
            };

            _context.Add(_statusReportEntity);
            await _context.SaveChangesAsync();

    
        }
    }
}
