using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorSystem.Models
{
    public class StatusReport
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; } = "Ej påbörjad";
        public DateTime DateUpdated { get; set; }

    }
}
