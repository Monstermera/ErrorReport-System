using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ErrorSystem.Models.Entities
{
    internal class CustomerEntity
    {
        [Key]
        public int CustomId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string FirstName { get; set; } = string.Empty;
            
        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "char(13)")]
        public string PhoneNumber { get; set; } = string.Empty;

        // ADDRESS
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;



        public ICollection<ErrorReportEntity> ErrorReports = new HashSet<ErrorReportEntity>();
        public ICollection<CommentEntity> Comments = new HashSet<CommentEntity>();
    }
}
