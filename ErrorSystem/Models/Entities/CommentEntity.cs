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
    internal class CommentEntity
    {
        [Key]
        public int CommentId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CommentText { get; set; } = string.Empty;
        public DateTime CommentCreated {  get; set; }


        // CUSTOMER
        [Required]
        public int CustomId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;


        public ICollection<ErrorReportEntity> ErrorReports = new HashSet<ErrorReportEntity>();

    }
}
