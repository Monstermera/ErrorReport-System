using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ErrorSystem.Models.Entities
{
    internal class TechnicianEntity
    {
        [Key]
        public int TechnicianId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string TechFirstName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(20)")]
        public string TechLastName { get; set; } = string.Empty;

        [Column(TypeName = "char(13)")]
        public string TechPhoneNumber { get; set; } = string.Empty;

        public ICollection<ErrorReportEntity> ErrorReports = new HashSet<ErrorReportEntity>();


    }
}
