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
    internal class ErrorReportEntity
    {
        [Key]
        public int ErrorId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; } = string.Empty;
        public DateTime DateReported { get; set; }


        // CUSTOMER
   
        [Required]
        public int CustomId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        // STATUS

        [Required]
        public int StatusId { get; set; }
        public StatusReportEntity StatusReport { get; set; } = null!;

        // COMMENT
        public int?  CommentId { get; set; }
        public CommentEntity? Comment { get; set; }

        // TECHNICIAN
        public int? TechnicianId { get; set; }
        public TechnicianEntity? Technician { get; set; }


        public ErrorReportEntity()
        {
            StatusReport = new StatusReportEntity
            {
                StatusName = "Ej Påbörjad",
                DateUpdated = DateTime.Now
            };
        }

        public ICollection<StatusReportEntity> StatusReports = new HashSet<StatusReportEntity>();



    }
}
