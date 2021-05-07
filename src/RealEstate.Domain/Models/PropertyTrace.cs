using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Models
{
    public class PropertyTrace
    {
        public int Id { get; set; }

        public DateTimeOffset DateSale { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Tax { get; set; }

        public int? PropertyId { get; set; }

        public Property Property { get; set; }
    }
}
