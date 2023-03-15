using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ErrorSystem.Models.Entities
{
    internal class StatusReportEntity
    {
        [Key]
        public int StatusId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string StatusName { get; set; } = string.Empty;
        public DateTime DateUpdated { get; set; }

        public ICollection<ErrorReportEntity> ErrorReports = new HashSet<ErrorReportEntity>();
    }
}
