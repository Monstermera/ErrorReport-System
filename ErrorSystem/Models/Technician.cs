using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorSystem.Models.Entities;

namespace ErrorSystem.Models
{
    internal class Technician
    {
        public int TechnicianId { get; set; }
        public string TechFirstName { get; set; } = string.Empty;
        public string TechLastName { get; set; } = string.Empty;
        public string TechPhoneNumber { get; set; } = string.Empty;

    }
}
