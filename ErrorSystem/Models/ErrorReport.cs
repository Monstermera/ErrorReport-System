using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ErrorSystem.Models
{
    internal class ErrorReport
    {
        public int ErrorId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DateReported { get; set; }

        // CUSTOMER
        public int CustomId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        // STATUS
        public int StatusId { get; set; }
        public string StatusName { get; set; } = "Ej påbörjad";
        public DateTime DateUpdated { get; set; }

        // TECHNICIAN
        public int? TechnicianId { get; set; }
        public string? TechFirstName { get; set; } = string.Empty;
        public string? TechLastName { get; set; } = string.Empty;

        // COMMENT
        public int? CommentId { get; set; }
        public string? CommentText { get; set; } = string.Empty;
        public DateTime?  CommentCreated { get; set; }

    }
}
