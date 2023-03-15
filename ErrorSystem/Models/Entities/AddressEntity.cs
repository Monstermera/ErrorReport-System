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
    internal class AddressEntity
    {
        [Key]
        public int AddressId { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string StreetName { get; set; } = string.Empty;

        [Column(TypeName = "char(6)")]
        public string ZipCode { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(30)")]
        public string City { get; set; } = string.Empty;

        public ICollection<CustomerEntity> Customers = new HashSet<CustomerEntity>();

    }
}
